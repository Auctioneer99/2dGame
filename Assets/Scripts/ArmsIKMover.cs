using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    [Serializable]
    public class HumanBone
    {
        public HumanBodyBones Bone;
        public float Weight = 1.0f;
    }

    public class ArmsIKMover : MonoBehaviour
    {
        [SerializeField]
        protected Animator animator;

        public Transform target = null;
        public Transform aim = null;

        public int iterations = 10;
        public float weight = 1.0f;

        public HumanBone[] humanBones;
        Transform[] boneTransforms;

        private void Start()
        {
            boneTransforms = humanBones.Select(b => animator.GetBoneTransform(b.Bone)).ToArray();
        }

        private void LateUpdate()
        {
            Vector3 targetPosition = target.position;
            for(int i = 0; i < iterations; i++)
            {
                for (int b = 0; b < boneTransforms.Length; b++)
                {
                    Transform bone = boneTransforms[b];
                    float boneWeight = humanBones[b].Weight * weight;
                    AimAtTarget(bone, targetPosition, boneWeight);
                }
            }
        }

        private void AimAtTarget(Transform bone, Vector3 targetPosition, float weight)
        {
            Vector3 aimDirection = aim.forward;
            Vector3 targetDirection = targetPosition - aim.position;
            Quaternion aimTowards = Quaternion.FromToRotation(aimDirection, targetDirection);
            Quaternion blendedRotation = Quaternion.Slerp(bone.rotation, aimTowards, weight);
            bone.rotation = blendedRotation;
        }
    }
}
