using UnityEngine;

public class MoveScript : MonoBehaviour
{
    private float timeTaken = 0.2f; // 動作にかかる時間
    private float timeElapsed; // 経過時間
    private Vector3 destination; // 目的地
    private Vector3 origin; // 出発地点

    private void Start()
    {
        destination = transform.position; // 初期位置
        origin = destination; // 出発地点も初期位置と同じ
    }

    public void MoveTo(Vector3 newDestination)
    {
        timeElapsed = 0; // 経過時間をリセット
        origin = destination; // 現在の目的地を出発地点に設定
        destination = newDestination; // 新しい目的地を設定
    }

    private void Update()
    {
        //if (origin == destination) return; // 既に目的地にいる場合は何もしない

        //timeElapsed += Time.deltaTime; // 経過時間を増加
        //float timeRate = timeElapsed / timeTaken; // 時間の割合を計算

        //// timeRateを0から1の範囲に制限
        //timeRate = Mathf.Clamp01(timeRate);

        //// 位置を補間
        //transform.position = Vector3.Lerp(origin, destination, timeRate);

        //// 目的地に到達したかを確認
        //if (timeRate >= 1)
        //{
        //    origin = destination; // 新たな移動が開始されるまで更新を停止
        //}

        // 目的地に到着していたら処理しない

        if (origin == destination) { return; }

        //時間経過を加算
        timeElapsed += Time.deltaTime;
        
        // 経過時間が完了時間の何割かを算出
        float timeRate = timeElapsed / timeTaken;
        
        // 完了時間を超えるようであれば実行完了時間相当に丸める。
        if (timeRate > 1) { timeRate = 1; }
        
        // イージング用計算(リニア)
        float easing = timeRate;
        
        //座標を算出
        Vector3 currentPosition = Vector3.Lerp(origin, destination, easing);
        
        // 算出した座標をpositionに代入
        transform.position = currentPosition;
    }
}
