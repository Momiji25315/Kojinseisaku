using System.Collections.Generic;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    [Header("�n�`�p�^�[��")]
    public GameObject[] terrainPatterns;
    public GameObject safePatternPrefab; // �� �V�@�\�F���S�Ȓn�`�uTemplate3�v�������ɐݒ� ��

    [Header("�����ݒ�")]
    public float scrollSpeed = 5f;
    public float spawnY = 0f;
    public float safeZoneDuration = 2.0f; // �� �V�@�\�F���S�n�т������b�� ��

    // --- �����ϐ� ---
    private List<GameObject> activePatterns = new List<GameObject>();
    private Camera mainCamera;
    private GameObject lastSpawnedPattern;
    private float timeSinceStart = 0f; // �Q�[���J�n����̌o�ߎ���
    private bool isSafeZone = true;    // ���݂����S�n�т��ǂ���

    void Start()
    {
        mainCamera = Camera.main;

        // �ŏ��̃p�^�[���Ƃ��āA���S�Ȓn�`�����_�ɔz�u
        GameObject initialPattern = Instantiate(safePatternPrefab, new Vector3(0, spawnY, 0), Quaternion.identity);
        activePatterns.Add(initialPattern);
        lastSpawnedPattern = initialPattern;
    }

    void Update()
    {
        // �Q�[���J�n����̌o�ߎ��Ԃ��v��
        timeSinceStart += Time.deltaTime;

        // �����A���S�n�т̎��Ԃ��I��������A�t���O��؂�ւ���
        if (isSafeZone && timeSinceStart > safeZoneDuration)
        {
            isSafeZone = false;
        }

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
            float rightEdge = GetPatternRightEdge(lastSpawnedPattern);
            if (rightEdge < mainCamera.ViewportToWorldPoint(new Vector3(1, 1, 0)).x)
            {
                SpawnNextPattern();
            }
        }

        // ��ʊO�ɏo���Â��p�^�[�����폜���鏈�����Y�ꂸ�ɒǉ����܂��傤
        // (���̕����͕ʓr�ǉ����K�v�ł�)
    }

    void SpawnNextPattern()
    {
        GameObject selectedPatternPrefab;

        // ������ �������V�������W�b�N�ł� ������
        // �����A�܂����S�n�тȂ�c
        if (isSafeZone)
        {
            // �c�����I�Ɉ��S�ȃp�^�[����I��
            selectedPatternPrefab = safePatternPrefab;
        }
        // �����łȂ���΁c
        else
        {
            // �c����܂Œʂ�A�z�񂩂烉���_���ɑI��
            int randomIndex = Random.Range(0, terrainPatterns.Length);
            selectedPatternPrefab = terrainPatterns[randomIndex];
        }

        // 1. �Ō�ɐ��������p�^�[���́uEndPoint�v��T��
        Transform endPoint = lastSpawnedPattern.transform.Find("EndPoint");
        if (endPoint == null)
        {
            Debug.LogError("�p�^�[���� 'EndPoint' ��������܂���I: " + lastSpawnedPattern.name);
            return;
        }

        // 2. �I�΂ꂽ�p�^�[���𐶐�����
        GameObject newPattern = Instantiate(selectedPatternPrefab);

        // 3. �V�����p�^�[���̍��[���A�O�̃p�^�[����EndPoint�ɂ҂����荇�킹��
        float newPatternWidth = GetPatternWidth(newPattern);
        float newPatternLeftOffset = newPatternWidth / 2;
        newPattern.transform.position = new Vector3(endPoint.position.x + newPatternLeftOffset, spawnY, 0);

        // 4. ���X�g�ɒǉ����A�u�Ō�̃p�^�[���v�Ƃ��ċL������
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