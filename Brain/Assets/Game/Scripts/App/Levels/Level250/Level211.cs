using BaseFramework;

public class Level211 : InputNumLevel {

	protected override void Awake()
	{
		this.answer = Localization.GetText("level_121_password");
		base.Awake();
	}
}
