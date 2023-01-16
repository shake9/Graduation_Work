using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gestureTest : MonoBehaviour
{
    Animator animator;
    AudioSource audioSource;
    public AudioClip sound1;
    public AudioClip sound2;
    public bool IsL;
    private bool fire = false;
    private bool charge = false;
    private bool Lsound = false;
    private bool Rsound = false;
    private float energy = 0.0f;

    [SerializeField]
    private OVRSkeleton _skeleton; //�E��A�������͍���� Bone���
    private OVRHand _oVRHand;

    /// <summary>
    /// �w�肵���S�Ă�BoneID��������ɂ��邩�ǂ������ׂ�
    /// </summary>
    /// <param name="threshold">臒l 1�ɋ߂��قǌ�����</param>
    /// <param name="boneids"></param>
    /// <returns></returns>
    private bool IsStraight(float threshold, params OVRSkeleton.BoneId[] boneids)
    {
        if (boneids.Length < 3) return false;   //���ׂ悤���Ȃ�
        Vector3? oldVec = null;
        var dot = 1.0f;
        for (var index = 0; index < boneids.Length - 1; index++)
        {
            var v = (_skeleton.Bones[(int)boneids[index + 1]].Transform.position - _skeleton.Bones[(int)boneids[index]].Transform.position).normalized;
            if (oldVec.HasValue)
            {
                dot *= Vector3.Dot(v, oldVec.Value); //���ς̒l�𑍏悵�Ă���
            }
            oldVec = v;//�ЂƂO�̎w�x�N�g��
        }
        return dot >= threshold; //�w�肵��BoneID�̓��ς̑��悪臒l�𒴂��Ă����璼���Ƃ݂Ȃ�
    }

    // Start is called before the first frame update
    void Start()
    {
        _oVRHand = GetComponent<OVRHand>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // �n���h�g���b�L���O���I���ɂȂ��Ă��Ȃ��ƒʂ�Ȃ�
        if (!_oVRHand.IsTracked || _oVRHand.HandConfidence.Equals(OVRHand.TrackingConfidence.Low)) return;

        // �e�w
        var isThumbStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Thumb0, OVRSkeleton.BoneId.Hand_Thumb1, OVRSkeleton.BoneId.Hand_Thumb2, OVRSkeleton.BoneId.Hand_Thumb3, OVRSkeleton.BoneId.Hand_ThumbTip);
        // �l�����w
        var isIndexStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Index1, OVRSkeleton.BoneId.Hand_Index2, OVRSkeleton.BoneId.Hand_Index3, OVRSkeleton.BoneId.Hand_IndexTip);
        // ���w
        var isMiddleStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Middle1, OVRSkeleton.BoneId.Hand_Middle2, OVRSkeleton.BoneId.Hand_Middle3, OVRSkeleton.BoneId.Hand_MiddleTip);
        // ��w
        var isRingStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Ring1, OVRSkeleton.BoneId.Hand_Ring2, OVRSkeleton.BoneId.Hand_Ring3, OVRSkeleton.BoneId.Hand_RingTip);
        // ���w
        var isPinkyStraight = IsStraight(0.8f, OVRSkeleton.BoneId.Hand_Pinky0, OVRSkeleton.BoneId.Hand_Pinky1, OVRSkeleton.BoneId.Hand_Pinky2, OVRSkeleton.BoneId.Hand_Pinky3, OVRSkeleton.BoneId.Hand_PinkyTip);

        // �e�w�����������Ă���
        if (isIndexStraight && isMiddleStraight && isRingStraight && isPinkyStraight && isThumbStraight && !IsL)
        {
            Debug.Log("fire");
            fire = true;
            //animator.SetBool("Fire", true);
            //audioSource.PlayOneShot(sound1);
        }
        // �e�w�Ɛl�����w�������Ă���
        else if (!isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight && !isThumbStraight && !IsL)
        {
            Debug.Log("reload");
            fire = false;
            //animator.SetBool("Fire", false);

        }

        // �e�w�֌W�Ȃ�����ȊO�̎w���S�ė����Ă���
        if (isIndexStraight && isMiddleStraight && isRingStraight && isPinkyStraight && IsL)
        {
            Debug.Log("charge");
            charge = true;
            if (energy <= 1.0f)
            {
                energy += 0.01f;
            }
            else
            {
                energy = 1.0f;
            }

            //animator.SetBool("Charge", true);
        }
        else if (!isIndexStraight && !isMiddleStraight && !isRingStraight && !isPinkyStraight && IsL)
        {
            Debug.Log("charge");
            charge = false;
            //animator.SetBool("Charge", false);
        }



        if (Rsound && fire)
        {
            audioSource.PlayOneShot(sound1);
            Rsound = false;
        }

        if (!fire)
        {
            Rsound = true;
        }

        if (Lsound && charge)
        {
            audioSource.PlayOneShot(sound2);
            Lsound = false;
        }

        if (!charge)
        {
            Lsound = true;
        }

        animator.SetBool("Fire", fire);
        animator.SetBool("Charge", charge);
        animator.SetFloat("Energy", energy);
    }
}