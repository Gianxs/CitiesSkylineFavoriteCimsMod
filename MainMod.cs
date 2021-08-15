using UnityEngine;
using ICities;

namespace FavoriteCims
{
	public class FavoriteCimsModMain : IUserMod
	{
		public string Name { get { return "Favorite Cims v0.4"; } }
		public string Description { get { return "Allows you to add and show favorite citizens in a list."; } }
		public const string Version = "v0.4";
	}
}