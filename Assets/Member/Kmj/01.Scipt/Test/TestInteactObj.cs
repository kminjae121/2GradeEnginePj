using UnityEngine;

namespace Member.Kmj._01.Scipt.Test
{
    public class TestInteactObj : InteractionObj
    {
        protected override void Awake()
        {
            base.Awake();
        }

        public override void HandleInteractEvent()
        {
            Debug.Log("아잇");
        }
    }
}