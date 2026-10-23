using UnityEngine;

public class Blobify : MonoBehaviour
{
    [SerializeField] private Transform entity;
    [SerializeField] private float radius = 4f;
    [SerializeField] private LineRenderer lineRenderer;
    [SerializeField] private int circlePoints = 14;
    [SerializeField] private float NoteSpawnrate = 1.5f;
    [SerializeField] private float speed = 10f;
    [SerializeField] private GameObject musicnotePrefab;
    [SerializeField] private int NoteCount = 5;
    [SerializeField] private float NoteInterval = 0.3f;
    [SerializeField] private float NotePerBurst = 1f;
    [SerializeField] private Sprite upgrade2;
    [SerializeField] private Sprite upgrade3;

    private SpriteRenderer spriteRenderer; public float NoteLifetime = 0.5f;
    private bool entityInRadius;
    private float nextNoteTime = 0f;
    private int notesShot = 0;

    void FindEntities()
    {
        if (entity == null)
        {
            GameObject normal = GameObject.FindGameObjectWithTag("Entity");

            if (normal != null)
                entity = normal.transform;
        }
    }

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        FindEntities();
        if (entity == null) return;

        DrawSightLine();

        bool inRadius = IsEntityInRadius();

        if (inRadius && Time.time > nextNoteTime)
        {
            burst();
            notesShot++;

            if(notesShot >= NotePerBurst)
            {
                notesShot = 0;
                nextNoteTime = Time.time + NoteSpawnrate;
            }
            else
            {
                nextNoteTime = Time.time + NoteInterval;
            }
        }
    }

    bool IsEntityInRadius()
    {
        float distance = Vector2.Distance(transform.position, entity.position);
        return distance <= radius;
    }

    public void burst()
    {
        float angleStep = 360f / NoteCount;
        float angle = 0f;

        for (int i = 0; i < NoteCount; i++)
        {
            float projectileDirXPosition = transform.position.x + Mathf.Sin((angle * Mathf.PI) / 180);
            float projectileDirYposition = transform.position.y + Mathf.Cos((angle * Mathf.PI) / 180);

            Vector2 direction = new Vector2(projectileDirXPosition, projectileDirYposition) - new Vector2(transform.position.x, transform.position.y);

            GameObject note = Instantiate(musicnotePrefab, transform.position, Quaternion.identity);
            note.GetComponent<MusicNoteProjectile>().LaunchNoteDirection(direction, speed, entity);
            Destroy(note, NoteLifetime);

            angle += angleStep;
        }
    }

    public void Upgrade2_Blobify()
    {
        NoteCount++;
        radius += 1f;
        NoteSpawnrate += 1.1f;
        NoteLifetime += 0.1f;
        NotePerBurst += 1f;
        spriteRenderer.sprite = upgrade2;
    }

    public void Upgrade3_Blobify()
    {
        if (NoteCount < 8)
        {
            NoteCount++;
            radius += 1f;
            NoteSpawnrate += 1.1f;
            NoteLifetime += 0.1f;
            NotePerBurst += 1f;
            spriteRenderer.sprite = upgrade3;
        }
    }

    void DrawSightLine()
    {
        if (lineRenderer == null || circlePoints < 1) return;
        lineRenderer.positionCount = circlePoints + 1;
        Vector3 center = transform.position;

        for (int i = 0; i <= circlePoints; i++)
        {
            float angle = (360f / circlePoints) * i;
            float x = Mathf.Cos(angle * Mathf.Deg2Rad) * radius;
            float y = Mathf.Sin(angle * Mathf.Deg2Rad) * radius;

            Vector3 point = center + new Vector3(x, y, 0f);

            lineRenderer.SetPosition(i, point);
        }
    }
}