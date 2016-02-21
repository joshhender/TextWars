using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using UnityEngine.SceneManagement;
using System.Collections;

public class UIManager : MonoBehaviour {

    public VerticalScrollSnap player1Faces, player2Faces;

    public void LoadArena()
    {
        GameManager.instance.player1Face = player1Faces.CurrentScreen();
        GameManager.instance.player2Face = player2Faces.CurrentScreen();        
        SceneManager.LoadScene("Arena");
    }
}
