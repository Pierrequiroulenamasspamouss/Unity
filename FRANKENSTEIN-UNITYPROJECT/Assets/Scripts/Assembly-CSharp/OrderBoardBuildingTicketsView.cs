public class OrderBoardBuildingTicketsView : global::UnityEngine.MonoBehaviour
{
	public global::UnityEngine.GameObject[] Tickets;

	private float oneThird = 0.3333334f;

	private float twoThird = 2f / 3f;

	internal void DisableTickets()
	{
		global::UnityEngine.GameObject[] tickets = Tickets;
		foreach (global::UnityEngine.GameObject gameObject in tickets)
		{
			gameObject.SetActive(false);
		}
	}

	internal global::UnityEngine.Vector3 GetTicketPosition(int index)
	{
		return Tickets[index].transform.position;
	}

	internal bool IsOrderboardSetupCorrectly()
	{
		if (Tickets == null || Tickets.Length == 0)
		{
			return false;
		}
		return true;
	}

	internal void SetTicketState(int ticketIndex, global::Kampai.Game.OrderBoardTicketState state)
	{
		if (state == global::Kampai.Game.OrderBoardTicketState.NOT_AVAILABLE)
		{
			Tickets[ticketIndex].SetActive(false);
		}
		else
		{
			Tickets[ticketIndex].SetActive(true);
		}
		switch (state)
		{
		case global::Kampai.Game.OrderBoardTicketState.PRESTIGE_CHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(oneThird, twoThird);
			break;
		case global::Kampai.Game.OrderBoardTicketState.CHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(oneThird, 0f);
			break;
		case global::Kampai.Game.OrderBoardTicketState.UNCHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(0f, 0f);
			break;
		case global::Kampai.Game.OrderBoardTicketState.PRESTIGE_UNCHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(0f, twoThird);
			break;
		case global::Kampai.Game.OrderBoardTicketState.VILLAIN_CHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(oneThird, oneThird);
			break;
		case global::Kampai.Game.OrderBoardTicketState.VILLAIN_UNCHECKED:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(0f, oneThird);
			break;
		case global::Kampai.Game.OrderBoardTicketState.TIMER:
			Tickets[ticketIndex].renderer.material.mainTextureOffset = new global::UnityEngine.Vector2(twoThird, 0f);
			break;
		}
	}
}
