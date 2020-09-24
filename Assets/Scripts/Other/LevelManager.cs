using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Assets.Scripts.Other
{
	class LevelManager : MonoBehaviour
	{
		public static LevelManager Instance;
		public Image LevelCompleteImage;
		public LevelManager()
		{
			Instance = this;
		}
		public void RestartLevel()
		{
			SceneManager.LoadScene(0,LoadSceneMode.Single);
		}
		public void ShowEndingPicture()
		{
			LevelCompleteImage.gameObject.SetActive(true);
		}
		public void Quit()
		{
			Application.Quit();
		}
	}

}
