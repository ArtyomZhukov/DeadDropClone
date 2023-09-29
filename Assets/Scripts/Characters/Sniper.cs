using UnityEngine;

public class Sniper : MonoBehaviour
{
    [SerializeField] private int bulletsCount = 3;
    [SerializeField] private float reloadTime = 5.0f;
    [SerializeField] private float holdTime = 3.0f;

    [SerializeField] private TextMesh bulletsCountText = null;
    [SerializeField] private TextMesh noBulletsTipText = null;
    [SerializeField] private GameObject notify = null;
    [SerializeField] private AudioClip shootSound = null;
    [SerializeField] private AudioClip blankShootSound = null;

    private AudioSource audioSource;
    private int bullets = 0;

    private float reloadingTime = 0;
    private bool reloading = false;

    private float holdingTime = 0;
    private bool holding = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        bullets = bulletsCount;
        SetBulletsCountText();
        bulletsCountText.gameObject.GetComponent<MeshRenderer>().sortingLayerName =
        noBulletsTipText.gameObject.GetComponent<MeshRenderer>().sortingLayerName = "Sniper Layer";
    }

    void Update()
    {
        CursorMove();
        if (SceneManager.endGame || SceneManager.pause)
            return;

        if (reloading)
        {
            if (reloadingTime > 0)
            {
                reloadingTime -= Time.deltaTime;
                bulletsCountText.text = "RELOADING.. " + ((int)reloadingTime + 1);
            }
            else
            {
                SetBulletsCountText();
                reloading = false;
            }
            if (Input.GetButtonDown("Shoot"))
            {
                CreateNotify("RELOADING");
                audioSource.PlayOneShot(blankShootSound);
            }
        }
        else if (bullets <= 0)
        {
            CheckSurrender();
        }
        else if (Input.GetButtonDown("Shoot"))
        {
            if (bullets > 0)
            {
                Shoot();
                bullets--;
                if (bullets > 0)
                {
                    reloading = true;
                    reloadingTime = reloadTime;
                }
                else
                {
                    SetBulletsCountText();
                }
            }
        }
    }

    private void CursorMove()
    {
        Cursor.visible = false;
        Vector3 mousePosition = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5);
        Vector3 objPosition = Camera.main.ScreenToWorldPoint(mousePosition);
        transform.position = objPosition;
    }

    private void Shoot()
    {
        CameraShake.Shake(0.1f, 0.1f);
        audioSource.PlayOneShot(shootSound);
        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D[] raycasts = Physics2D.RaycastAll(ray, Vector2.zero);

        if (raycasts != null && raycasts.Length > 0)
        {
            foreach (RaycastHit2D hit in raycasts)
            {
                if (hit.collider.tag == "Character")
                {
                    CreateNotify("CIVILIAN");
                    hit.collider.GetComponent<Character>().Die();
                }
                if (hit.collider.tag == "Player")
                {
                    CreateNotify("SPY KILLED");
                    hit.collider.GetComponent<Character>().Die();
                    SetBulletsCountText();
                    SceneManager.winner = SceneManager.Player.Sniper;
                }
            }
        }
    }

    private void CheckSurrender()
    {
        if (Input.GetButtonDown("Shoot"))
        {
            holdingTime = 0;
            holding = true;
        }
        else if (Input.GetButtonUp("Shoot"))
        {
            if (holdingTime < 1)
            {
                audioSource.PlayOneShot(blankShootSound);
                CreateNotify("X");
            }
            noBulletsTipText.text = "HOLD LEFT CLICK\nTO SURRENDER";
            holding = false;
        }

        if (holding)
        {
            holdingTime += Time.deltaTime;
            if (holdingTime < 1)
                return;
            noBulletsTipText.text = "HOLDING.." + (int)holdingTime;
            if (holdingTime > holdTime + 1)
            {
                noBulletsTipText.text = "";
                SceneManager.winner = SceneManager.Player.Spy;
            }
        }
    }

    private void SetBulletsCountText()
    {
        if (bullets >= 1)
        {
            bulletsCountText.text = "SHOOTS: " + bullets;
        }
        else
        {
            bulletsCountText.text = "NO SHOOTS";
            noBulletsTipText.gameObject.SetActive(true);
        }
    }

    private void CreateNotify(string text)
    {
        Vector3 pos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 5));
        GameObject notifyObj = Instantiate(notify, pos, Quaternion.identity);
        notifyObj.GetComponent<NotifyText>().SetText(text);
    }
}
