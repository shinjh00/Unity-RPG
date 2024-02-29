using System.Collections;
using UnityEngine;

public class GameScene : BaseScene
{
    [SerializeField] Monster monsterPrefab;
    [SerializeField] Transform spawnPoint;
    [SerializeField] int count;

    public override IEnumerator LoadingRoutine()
    {
        Debug.Log("GameScene Load");
        // fake loading

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("Player Spawn");

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("오브젝트 풀 준비");

        yield return new WaitForSecondsRealtime(0.5f);

        for (int i = 0; i < count; i++)
        {
            Vector2 randomOffset = Random.insideUnitCircle * 3;
            Vector3 spawnPos = spawnPoint.position + new Vector3(randomOffset.x, 0, randomOffset.y);
            Monster monster = Instantiate(monsterPrefab, spawnPos, Quaternion.identity);

            Debug.Log("몬스터 스폰");
            yield return null;
        }

        yield return new WaitForSecondsRealtime(0.5f);
        Debug.Log("GameScene Loading......");
    }

    public void ToTitleScene()
    {
        Manager.Scene.LoadScene("TitleScene");
    }
}
