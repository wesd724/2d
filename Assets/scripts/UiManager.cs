using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    public moveUI completeWindow;
    public moveUI completeExplain;

    public moveUI readyWindow;
    public moveUI readyExplain;

    public moveUI roundSelect;
    public moveUI roundExplain;

    public moveUI failWindow;
    public moveUI failExplain;

    public moveUI endWindow;
    public moveUI endExplain;

    public moveUI startWindow;
    public moveUI startExplain;

    public GameObject gameManager;
    public readyManager readyManager;

    public void completeWindowOpen() // 목표 달성 완료 창
    {
        string goal = TextManager.instance.goal.text;
        transform.GetChild(2).GetComponent<completeManager>().open(goal);
        StartCoroutine(completeExplain.explainDown());
        StartCoroutine(completeWindow.moveUp(-175));
    }

    public void readyWindowOpen() // 준비 (상점) 창 
    {
        readyWindow.transform.parent.gameObject.SetActive(true);
        StartCoroutine(organize());
    }

    IEnumerator organize()
    {
        StartCoroutine(completeExplain.moveUp(770));
        yield return StartCoroutine(completeWindow.moveDown(-1015));
        yield return StartCoroutine(GameManager.instance.organize());
        StartCoroutine(readyWindowAnimation());
    }


    IEnumerator readyWindowAnimation()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(readyExplain.explainDown());
        readyManager.showBack();
        yield return StartCoroutine(readyWindow.moveUp(185));
        yield return new WaitForSecondsRealtime(0.05f);
        StartCoroutine(readyManager.show());
    }

    public void roundSelectOpen() // 라운드 선택 창
    {
        roundSelect.transform.parent.gameObject.SetActive(true);
        StartCoroutine(readyExplain.moveUp(770));
        StartCoroutine(roundSelectAnimation());
    }

    IEnumerator roundSelectAnimation()
    {
        yield return StartCoroutine(readyWindow.moveDown(-1015));
        yield return new WaitForSecondsRealtime(0.3f);
        StartCoroutine(roundExplain.explainDown());
        StartCoroutine(roundSelect.moveUp(-175));
    }

    public void select()
    {
        StartCoroutine(roundExplain.moveUp(770));
        StartCoroutine(selectAnimation());
    }

    IEnumerator selectAnimation()
    {
        yield return StartCoroutine(roundSelect.moveDown(-1015));
        StartCoroutine(GameManager.instance.startGame());
    }

    public void failWindowOpen() // 모든 라운드 완료 창
    {
        failWindow.transform.parent.gameObject.SetActive(true);
        StartCoroutine(failExplain.explainDown());
        StartCoroutine(failWindow.moveUp(-175));
    }

    public void restart() // 다시 시작하기
    {
        StartCoroutine(re());
    }

    IEnumerator re()
    {
        StartCoroutine(failExplain.moveUp(770));
        yield return StartCoroutine(failWindow.moveDown(-1015));
        gameInit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void endWindowOpen() // 끝 창
    {
        endWindow.transform.parent.gameObject.SetActive(true);
        StartCoroutine(endExplain.explainDown(0));
        StartCoroutine(endWindow.moveUp(-175));
    }

    public void restart2() 
    {
        StartCoroutine(re2());
    }

    IEnumerator re2()
    {
        StartCoroutine(endExplain.moveUp(1100));
        yield return StartCoroutine(endWindow.moveDown(-1015));
        gameInit();
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void startWindowClose() // 시작화면에서 시작하기 버튼 클릭
    {
        StartCoroutine(re3());
    }

    IEnumerator re3()
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
        GameManager.wave = 0;
        GameManager.round = -1;
        TextManager.instance = null;
        serviceAndUpgrade.initS();
        serviceAndUpgrade.initU();
        // audoManger와 jokbolist는 초기화 안함.

    }

    public void quit() // 게임 종료
    {
        Application.Quit();
    }






    // 아직 고려 안함
    public void readyWindowClose()
    {
        StartCoroutine(readyWindow.moveDown(-1015));
    }

    public void roundSelectClose()
    {
        StartCoroutine(roundSelect.moveDown(-1015));
    }

    public void completeWindowClose()
    {
        StartCoroutine(completeWindow.moveDown(-1015));
    }
}
