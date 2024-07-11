using CoverShooter;
using TouchControlsKit;
using UnityEngine;

public class Laser : MonoBehaviour
{
    [SerializeField] private GameObject _laser;
    [SerializeField] private Transform _origin;
    [SerializeField] private Transform _aim;
    [SerializeField] private LineRenderer _lineRenderer;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _laserSound;
    [SerializeField] private BaseGun _gun;

    private void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            if (TCKInput.GetButton(InputParametrs.Fire) && _gun.IsAllowed)
            {
                if (_audioSource.isPlaying == false)
                {
                   _audioSource.Play();
                }

                _laser.SetActive(true);

                RaycastHit hit;

                if (Physics.Raycast(_origin.position, _aim.forward * 10, out hit))
                {
                    if (hit.collider)
                    {
                        _lineRenderer.SetPosition(0, _origin.position);
                        _lineRenderer.SetPosition(1, hit.point);
                    }
                }
                else
                {
                    _lineRenderer.SetPosition(0, _origin.position);
                    _lineRenderer.SetPosition(1, _aim.forward * 5000);
                }
            }
            else if (TCKInput.GetButtonUp(InputParametrs.Fire) || _gun.IsAllowed == false)
            {
                _laser.SetActive(false);
                _audioSource.Stop();
            }

        }
    }
}
