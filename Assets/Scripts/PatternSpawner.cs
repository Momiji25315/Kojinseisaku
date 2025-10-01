using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [Header("�n�`�p�^�[��")]
    public GameObject[] terrainPatterns;

    [Header("�����ݒ�")]
    public float scrollSpeed = 5f;
    public float spawnY = 0f;

    // --- �����ϐ� ---
    private List<GameObject> activePatterns = new List<GameObject>();
    private Camera mainCamera;
    private GameObject lastSpawnedPattern; // �Ō�ɐ��������p�^�[�����o���Ă���

    void Start()
    {
        mainCamera = Camera.main;

        // �ŏ��̃p�^�[�������_�ɔz�u
        GameObject initialPattern = Instantiate(terrainPatterns[0], new Vector3(0, spawnY, 0), Quaternion.identity);
        activePatterns.Add(initialPattern);
        lastSpawnedPattern = initialPattern;
    }

    void Update()
    {
        // �S�Ẵp�^�[�������ɓ�����
        foreach (GameObject pattern in activePatterns)
        {
            if (pattern != null)
            {
                pattern.transform.position += Vector3.left * scrollSpeed * Time.deltaTime;
            }
        }

        // �Ō�ɐ��������p�^�[���̉E�[����ʓ��ɓ����Ă�����A���̃p�^�[���𐶐�����
        if (lastSpawnedPattern != null)
        {
            // �p�^�[���̉E�[�̈ʒu���擾
            float rightEdge = GetPatternRightEdge(lastSpawnedPattern);
            if (rightEdge < mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x)
            {
                SpawnNextPattern();
            }
        }

        // ��ʊO�ɏo���Â��p�^�[�����폜����
        // (�ȗ�... �O��̃R�[�h�Ɠ����ō\���܂���)
    }

    void SpawnNextPattern()
    {
        // �ǂ̃p�^�[�����o���������_���ɑI��
        int randomIndex = Random.Range(0, terrainPatterns.Length);
        GameObject selectedPatternPrefab = terrainPatterns[randomIndex];

        // ������ �������V�����A�����W�b�N�ł� ������
        // 1. �Ō�ɐ��������p�^�[���́uEndPoint�v��T��
        Transform endPoint = lastSpawnedPattern.transform.Find("EndPoint");
        if (endPoint == null)
        {
            Debug.LogError("�p�^�[���� 'EndPoint' ��������܂���I: " + lastSpawnedPattern.name);
            return;
        }

        // 2. �V�����p�^�[���𐶐�����
        GameObject newPattern = Instantiate(selectedPatternPrefab);

        // 3. �V�����p�^�[���̍��[���A�O�̃p�^�[����EndPoint�ɂ҂����荇�킹��
        //    (�V�����p�^�[���̕��̔������l�����܂�)
        float newPatternWidth = GetPatternWidth(newPattern);
        float newPatternLeftOffset = newPatternWidth / 2;
        newPattern.transform.position = new Vector3(endPoint.position.x + newPatternLeftOffset, spawnY, 0);

        // 4. �V�����p�^�[�������X�g�ɒǉ����A�u�Ō�̃p�^�[���v�Ƃ��ċL������
        activePatterns.Add(newPattern);
        lastSpawnedPattern = newPattern;
    }

    // �p�^�[���̕���Collider����擾
    float GetPatternWidth(GameObject pattern)
    {
        BoxCollider2D collider = pattern.GetComponent<BoxCollider2D>();
        if (collider != null) return collider.size.x * pattern.transform.localScale.x;
        Debug.LogWarning("���𑪂邽�߂�BoxCollider2D������܂���: " + pattern.name);
        return 10f;
    }

    // �p�^�[���̃��[���h���W�ł̉E�[���擾
    float GetPatternRightEdge(GameObject pattern)
    {
        return pattern.transform.position.x + GetPatternWidth(pattern) / 2;
    }
}