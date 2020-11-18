using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using Mapbox.Utils;
using Mapbox.Unity.Location;


public class PlayerView : MonoBehaviourPun //, IPunObservable
{
    public Vector2d LatitudeLongitude;
    public string UserID;


    bool _isInitialized;

    ILocationProvider _locationProvider;
    ILocationProvider LocationProvider
    {
        get
        {
            if (_locationProvider == null)
            {
                _locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
            }

            return _locationProvider;
        }
    }

    Vector3 _targetPosition;

    void Start()
    {
        LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
    }

    void LateUpdate()
    {

        if (!this.photonView.IsMine && _isInitialized)
        {
            LatitudeLongitude = LocationProvider.CurrentLocation.LatitudeLongitude;
        }
    }


    /*
    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.IsWriting)
        {
            if (this.m_SynchronizePosition)
            {
                this.m_Direction = transform.localPosition - this.m_StoredPosition;
                this.m_StoredPosition = transform.localPosition;

                stream.SendNext(transform.localPosition);
                stream.SendNext(this.m_Direction);
            }

            if (this.m_SynchronizeRotation)
            {
                stream.SendNext(transform.localRotation);
            }

            if (this.m_SynchronizeScale)
            {
                stream.SendNext(transform.localScale);
            }
        }
        else
        {


            if (this.m_SynchronizePosition)
            {
                this.m_NetworkPosition = (Vector3)stream.ReceiveNext();
                this.m_Direction = (Vector3)stream.ReceiveNext();

                if (m_firstTake)
                {
                    transform.localPosition = this.m_NetworkPosition;
                    this.m_Distance = 0f;
                }
                else
                {
                    float lag = Mathf.Abs((float)(PhotonNetwork.Time - info.SentServerTime));
                    this.m_NetworkPosition += this.m_Direction * lag;
                    this.m_Distance = Vector3.Distance(transform.localPosition, this.m_NetworkPosition);
                }


            }

            if (this.m_SynchronizeRotation)
            {
                this.m_NetworkRotation = (Quaternion)stream.ReceiveNext();

                if (m_firstTake)
                {
                    this.m_Angle = 0f;
                    transform.localRotation = this.m_NetworkRotation;
                }
                else
                {
                    this.m_Angle = Quaternion.Angle(transform.localRotation, this.m_NetworkRotation);
                }
            }

            if (this.m_SynchronizeScale)
            {
                transform.localScale = (Vector3)stream.ReceiveNext();
            }

            if (m_firstTake)
            {
                m_firstTake = false;
            }
        }
    }
    */
}