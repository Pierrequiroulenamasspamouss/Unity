namespace Kampai.Game
{
	[global::Kampai.Util.RequiresJsonConverter]
	[global::Kampai.Util.Serializer("FastInstanceSerializationHelper.SerializeInstanceData")]
	public interface Instance : global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		global::Kampai.Game.Definition Definition { get; }
	}
	public interface Instance<T> : global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		new T Definition { get; }
	}
}
