using UnityEngine;

public class TestSkill : MonoBehaviour
{
    [SerializeField] private EntitySkillCompo _skillCompo;

    [SerializeField] private SkillSO _skillSO;
    private void Awake()
    {
        
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            _skillCompo.AddSkill(_skillSO);
        }
    }
}
