// --------------------------------------------------------------------------------------------------------------------
// --------------------------------------------------------------------------------------------------------------------

using Photon.Pun;
using Photon.Pun.Demo.PunBasics;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

	/// <summary>
	/// Player UI. Constraint the UI to follow a PlayerManager GameObject in the world,
	/// Affect a slider and text to display Player's name and health
	/// </summary>
	public class PlayerUI : MonoBehaviour
    {
        #region Private Fields

	    [Tooltip("Pixel offset from the player target")]
        [SerializeField]
        private Vector3 screenOffset = new Vector3(0f, 30f, 0f);

	    [Tooltip("UI Text to display Player's Name")]
	    [SerializeField]
	    private Text playerNameText;

        PlayerManager target;

        Transform targetTransform;

		Renderer targetRenderer;

	    CanvasGroup _canvasGroup;
	    
		Vector3 targetPosition;

		#endregion

		#region MonoBehaviour Messages
		
		
		void Update()
		{
			// Destroy itself if the target is null, It's a fail safe when Photon is destroying Instances of a Player over the network
			if (target == null) {
				Destroy(this.gameObject);
				return;
			}

		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Assigns a Player Target to Follow and represent.
		/// </summary>
		/// <param name="target">Target.</param>
		public void SetTarget(PlayerManager _target){

			if (_target == null) {
				Debug.LogError("<Color=Red><b>Missing</b></Color> PlayMakerManager target for PlayerUI.SetTarget.", this);
				return;
			}

			// Cache references for efficiency because we are going to reuse them.
			this.target = _target;
			targetRenderer = this.target.GetComponentInChildren<Renderer>();

			if (playerNameText != null) {
                playerNameText.text = target.photonView.Owner.NickName;
			}
		}

		#endregion

	}
