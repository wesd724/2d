using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public moveUI startWindow;
    public moveUI startExplain;

    public void startGame() // 시작화면에서 시작하기 버튼 클릭
    {
        StartCoroutine(re());
    }

    IEnumerator re()
    {
        StartCoroutine(startExplain.moveUp(1100));
        yield return StartCoroutine(startWindow.moveDown(-1015)); // 시작 -190
        //gameInit();
        SceneManager.LoadScene("Game");
    }

    void gameInit()
    {
        GameManager.instance = null;
        GameManager.d1 = false;
        GameManager.discardStack = 0; // 버리기카드 누적
        GameManager.d2 = false;
        GameManager.discardStack_s = 0; // 쓰레기통의 왕 카드 누적
        GameManager.round = -1;
        GameManager.wave = 0;
        TextManager.instance = null;
        serviceAndUpgrade.initS();
        serviceAndUpgrade.initU();
        // audoManger와 jokbolist는 초기화 안함.

    }
}
