public abstract class BaseData {
	public string GetAssetName(string name) => $"{name.Trim().ToUpper().Replace(" ", "_")}";
}
