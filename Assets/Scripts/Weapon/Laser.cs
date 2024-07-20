using CoverShooter;
using TouchControlsKit;
using UnityEngine;
using UnityEngine.UIElements;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _aim;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private BaseGun _gun;
    [SerializeField] private GameObject _newLaser;

    [Header ("Reload Parametrs")]
    [SerializeField] private float _delay;
    [SerializeField] private float _stopDelay;

    private float _elapsedTime;
    private float _elapsedStopTime;

    private bool _isStop;

    private Gun _gunForBullet;

    private void Start()
    {
        _gunForBullet = _gun.GetComponent<Gun>();
        _audioSource.clip = _laserSound;
    }

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (TCKInput.GetButton(InputParametrs.Fire) && _gun.IsAllowed && _gunForBullet.LoadedBullets > 0)
            {
                if (_audioSource.isPlaying == false)
                {
                   _audioSource.Play();
                }

                //_laser.SetActive(true);
                _newLaser.SetActive(true);

                RaycastHit hit;

                if (Physics.Raycast(_origin.position, _aim.forward * 10, out hit))
                {
                    Debug.Log(1111);
                    if (hit.collider)
                    {
                        _lineRenderer.SetPosition(0, _origin.position);
                        _lineRenderer.SetPosition(1, hit.point);
                    }
                }
                else
                {
                    Debug.Log(2222222);
                    _lineRenderer.SetPosition(0, _origin.position);
                    _lineRenderer.SetPosition(1, _aim.forward * 1000000);
                }
            }
            else if (TCKInput.GetButtonUp(InputParametrs.Fire) || _gun.IsAllowed == false || _gunForBullet.LoadedBullets <= 0)
            {
                // _laser.SetActive(false);
                _newLaser.SetActive(false);
                _audioSource.Stop();
            }
        }
        Reload();
    }

    private void Reload()
    {
        if (_gunForBullet.LoadedBullets <= 0 && _isStop == false)
        {
            _isStop = true;
        }

        if (_isStop == true)
        {
            _elapsedStopTime += Time.deltaTime;

            if (_elapsedStopTime >= _stopDelay)
            {
                _gunForBullet.FillMagazine(1);
                _elapsedStopTime = 0;
                _isStop = false;
            }
        }
        else
        {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime >= _delay)
            {
                _gunForBullet.FillMagazine(1);
                _elapsedTime = 0;
            }
        }
    }
}
