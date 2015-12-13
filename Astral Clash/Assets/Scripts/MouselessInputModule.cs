namespace UnityEngine.EventSystems
{
	[AddComponentMenu("Event/Mouseless Input Module")]
	public class MouselessInputModule : PointerInputModule
	{
		private float m_NextAction;
		
		protected MouselessInputModule()
		{}
		
		[SerializeField]
		private string m_HorizontalAxis = "Horizontal";
		
		/// <summary>
		/// Name of the vertical axis for movement (if axis events are used).
		/// </summary>
		[SerializeField]
		private string m_VerticalAxis = "Vertical";
		
		/// <summary>
		/// Name of the submit button.
		/// </summary>
		[SerializeField]
		private string m_SubmitButton = "Submit";
		
		/// <summary>
		/// Name of the submit button.
		/// </summary>
		[SerializeField]
		private string m_CancelButton = "Cancel";
		
		[SerializeField]
		private float m_InputActionsPerSecond = 10;
		
		[SerializeField]
		private bool m_AllowActivationOnMobileDevice;
		
		public bool allowActivationOnMobileDevice
		{
			get { return m_AllowActivationOnMobileDevice; }
			set { m_AllowActivationOnMobileDevice = value; }
		}
		
		public float inputActionsPerSecond
		{
			get { return m_InputActionsPerSecond; }
			set { m_InputActionsPerSecond = value; }
		}
		
		/// <summary>
		/// Name of the horizontal axis for movement (if axis events are used).
		/// </summary>
		public string horizontalAxis
		{
			get { return m_HorizontalAxis; }
			set { m_HorizontalAxis = value; }
		}
		
		/// <summary>
		/// Name of the vertical axis for movement (if axis events are used).
		/// </summary>
		public string verticalAxis
		{
			get { return m_VerticalAxis; }
			set { m_VerticalAxis = value; }
		}
		
		public string submitButton
		{
			get { return m_SubmitButton; }
			set { m_SubmitButton = value; }
		}
		
		public string cancelButton
		{
			get { return m_CancelButton; }
			set { m_CancelButton = value; }
		}
		
		public override bool ShouldActivateModule()
		{
			if (!base.ShouldActivateModule())
				return false;
			
			var shouldActivate = Input.GetButtonDown(m_SubmitButton);
			shouldActivate |= Input.GetButtonDown(m_CancelButton);
			shouldActivate |= !Mathf.Approximately(Input.GetAxisRaw(m_HorizontalAxis), 0.0f);
			shouldActivate |= !Mathf.Approximately(Input.GetAxisRaw(m_VerticalAxis), 0.0f);
			return shouldActivate;
		}
		
		public override void ActivateModule()
		{
			base.ActivateModule();
			
			var toSelect = eventSystem.currentSelectedGameObject;
			if (toSelect == null)
				toSelect = eventSystem.lastSelectedGameObject;
			if (toSelect == null)
				toSelect = eventSystem.firstSelectedGameObject;
			
			eventSystem.SetSelectedGameObject(toSelect, GetBaseEventData());
		}
		
		public override void DeactivateModule()
		{
			base.DeactivateModule();
			ClearSelection();
		}
		
		public override void Process()
		{
			bool usedEvent = SendUpdateEventToSelectedObject();
			
			if (eventSystem.sendNavigationEvents)
			{
				if (!usedEvent)
					usedEvent |= SendMoveEventToSelectedObject();
				
				if (!usedEvent)
					SendSubmitEventToSelectedObject();
			}		
		}
		
		/// <summary>
		/// Process submit keys.
		/// </summary>
		private bool SendSubmitEventToSelectedObject()
		{
			if (eventSystem.currentSelectedGameObject == null)
				return false;
			
			var data = GetBaseEventData();
			if (Input.GetButtonDown(m_SubmitButton))
				ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.submitHandler);
			
			if (Input.GetButtonDown(m_CancelButton))
				ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.cancelHandler);
			return data.used;
		}
		
		private bool AllowMoveEventProcessing(float time)
		{
			bool allow = Input.GetButtonDown(m_HorizontalAxis);
			allow |= Input.GetButtonDown(m_VerticalAxis);
			allow |= (time > m_NextAction);
			return allow;
		}
		
		private Vector2 GetRawMoveVector()
		{
			Vector2 move = Vector2.zero;
			move.x = Input.GetAxisRaw(m_HorizontalAxis);
			move.y = Input.GetAxisRaw(m_VerticalAxis);
			
			if (Input.GetButtonDown(m_HorizontalAxis))
			{
				if (move.x < 0)
					move.x = -1f;
				if (move.x > 0)
					move.x = 1f;
			}
			if (Input.GetButtonDown(m_VerticalAxis))
			{
				if (move.y < 0)
					move.y = -1f;
				if (move.y > 0)
					move.y = 1f;
			}
			return move;
		}
		
		/// <summary>
		/// Process keyboard events.
		/// </summary>
		private bool SendMoveEventToSelectedObject()
		{
			float time = Time.unscaledTime;
			
			if (!AllowMoveEventProcessing(time))
				return false;
			
			Vector2 movement = GetRawMoveVector();
			// Debug.Log(m_ProcessingEvent.rawType + " axis:" + m_AllowAxisEvents + " value:" + "(" + x + "," + y + ")");
			var axisEventData = GetAxisEventData(movement.x, movement.y, 0.6f);
			if (!Mathf.Approximately(axisEventData.moveVector.x, 0f)
			    || !Mathf.Approximately(axisEventData.moveVector.y, 0f))
			{
				ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, axisEventData, ExecuteEvents.moveHandler);
			}
			m_NextAction = time + 1f / m_InputActionsPerSecond;
			return axisEventData.used;
		}
		
		private bool SendUpdateEventToSelectedObject()
		{
			if (eventSystem.currentSelectedGameObject == null)
				return false;
			
			var data = GetBaseEventData();
			ExecuteEvents.Execute(eventSystem.currentSelectedGameObject, data, ExecuteEvents.updateSelectedHandler);
			return data.used;
		}
	}
}