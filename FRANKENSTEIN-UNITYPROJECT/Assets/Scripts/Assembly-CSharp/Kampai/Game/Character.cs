namespace Kampai.Game
{
	public interface Character : global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		string Name { get; set; }
	}
	public interface Character<T> : global::Kampai.Game.Character, global::Kampai.Game.Instance, global::Kampai.Util.IFastJSONDeserializable, global::Kampai.Util.IFastJSONSerializable, global::Kampai.Util.Identifiable
	{
		new T Definition { get; }
	}
}
