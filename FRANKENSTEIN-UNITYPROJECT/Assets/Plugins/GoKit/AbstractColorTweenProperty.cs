public abstract class AbstractColorTweenProperty : AbstractTweenProperty
{
	protected global::UnityEngine.Material _target;

	protected global::UnityEngine.Color _originalEndValue;

	protected global::UnityEngine.Color _startValue;

	protected global::UnityEngine.Color _endValue;

	protected global::UnityEngine.Color _diffValue;

	public AbstractColorTweenProperty(global::UnityEngine.Color endValue, bool isRelative)
		: base(isRelative)
	{
		_originalEndValue = endValue;
	}

	public override bool validateTarget(object target)
	{
		return target is global::UnityEngine.Material || target is global::UnityEngine.GameObject || target is global::UnityEngine.Transform || target is global::UnityEngine.Renderer;
	}

	public override void init(GoTween owner)
	{
		if (owner.target is global::UnityEngine.Material)
		{
			_target = (global::UnityEngine.Material)owner.target;
		}
		else if (owner.target is global::UnityEngine.GameObject)
		{
			_target = ((global::UnityEngine.GameObject)owner.target).renderer.material;
		}
		else if (owner.target is global::UnityEngine.Transform)
		{
			_target = ((global::UnityEngine.Transform)owner.target).renderer.material;
		}
		else if (owner.target is global::UnityEngine.Renderer)
		{
			_target = ((global::UnityEngine.Renderer)owner.target).material;
		}
		base.init(owner);
	}

	public override void prepareForUse()
	{
		if (_isRelative && !_ownerTween.isFrom)
		{
			_diffValue = _endValue;
		}
		else
		{
			_diffValue = _endValue - _startValue;
		}
	}
}
