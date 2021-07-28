using UnityEngine;
using TouchScript.Gestures.TransformGestures;

/// <summary>
/// プレイヤーの位置情報を取得し速度を計算するクラス
/// </summary>
public class PlayerController : MonoBehaviour
{
    // ジェスチャークラス
    [SerializeField] ScreenTransformGesture screenTransformGesture = default;
    // 瓦を割るためのコライダー
    [SerializeField] GameObject tileBreakCollider = default;
    // スクリーンチェンジャー
    [SerializeField] GameToResultScreenChanger gameToResultScreenChanger = default;
    // ポインターの場所を取得する時間
    [SerializeField] float pointerGetTime = default;

    // ドラッグ開始位置
    Vector2 startPosition;
    // ドラッグ終了位置
    Vector2 endPosition;

    // 時間計測
    float time;
    // 瓦に渡す速度
    float speed;
    public float Speed { get { return speed; } }

    /// <summary>
    /// 初期化処理
    /// </summary>
    void Start()
    {
        gameToResultScreenChanger.SetAction(Initialize);
        startPosition = new Vector2(0, 0);
        endPosition = new Vector2(0, 0);
        speed = 0.0f;
    }

    /// <summary>
    /// 有効化されたときの処理
    /// </summary>
    void OnEnable()
    {
        // Transform Gestureのdelegateに登録
        screenTransformGesture.StateChanged += StateChangedHandle;
        screenTransformGesture.Cancelled += CancelledHandle;
    }

    /// <summary>
    /// 無効化されたときの処理
    /// </summary>
    void OnDisable()
    {
        UnsubscribeEvent();
        tileBreakCollider.SetActive(false);
    }

    /// <summary>
    /// 削除されたときの処理
    /// </summary>
    void OnDestroy()
    {
        UnsubscribeEvent();
    }

    /// <summary>
    /// event削除処理
    /// </summary>
    void UnsubscribeEvent()
    {
        // 登録を解除
        screenTransformGesture.StateChanged -= StateChangedHandle;
        screenTransformGesture.Cancelled -= CancelledHandle;
    }

    /// <summary>
    /// ループ中に行う初期化処理
    /// </summary>
    void Initialize()
    {
        startPosition = new Vector2(0, 0);
        endPosition = new Vector2(0, 0);
        speed = 0.0f;
        time = 0.0f;
        tileBreakCollider.SetActive(false);
    }

    /// <summary>
    /// ドラッグ中の処理
    /// </summary>
    /// <param name="sender">送信者となるオブジェクト</param>
    /// <param name="e">イベント</param>
    void StateChangedHandle(object sender, System.EventArgs e)
    {
        ++time;

        if (time >= pointerGetTime)
        {
            startPosition = screenTransformGesture.ScreenPosition;
            time = 0.0f;
        }

        if(!tileBreakCollider.activeSelf)
        {
            tileBreakCollider.SetActive(true);
        }
    }

    /// <summary>
    /// 瓦に当たりドラッグが終了したときの処理
    /// </summary>
    /// <param name="sender">送信者となるオブジェクト</param>
    /// <param name="e">イベント</param>
    void CancelledHandle(object sender, System.EventArgs e)
    {
        endPosition = screenTransformGesture.ScreenPosition;
        SpeedCalculation();
        time = 0.0f;
    }

    /// <summary>
    /// 速度計算処理
    /// </summary>
    void SpeedCalculation()
    {
        float swipeLength = endPosition.y - startPosition.y;
        speed = Mathf.Abs(swipeLength);
        speed *= 10.0f;

        if(speed <= 1.0f)
        {
            speed = 1.0f;
        }
    }
}
