using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public moveUI startWindow;
    public moveUI startExplain;

    public void startGame() // ����ȭ�鿡�� �����ϱ� ��ư Ŭ��
    {
        StartCoroutine(re());
    }

    IEnumerator re()
    {
        StartCoroutine(startExplain.moveUp(1100));
        yield return StartCoroutine(startWindow.moveDown(-1015)); // ���� -190
        //gameInit();
        SceneManager.LoadScene("Game");
    }

    void gameInit()
    {
        GameManager.instance = null;
        GameManager.d1 = false;
        GameManager.discardStack = 0; // ������ī�� ����
        GameManager.d2 = false;
        GameManager.discardStack_s = 0; // ���������� �� ī�� ����
        GameManager.round = -1;
        GameManager.wave = 0;
        TextManager.instance = null;
        serviceAndUpgrade.initS();
        serviceAndUpgrade.initU();
        // audoManger�� jokbolist�� �ʱ�ȭ ����.

    }
}
