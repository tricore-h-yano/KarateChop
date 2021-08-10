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
    [SerializeField] PlayerHitChecker playerHitChecker = default;
    [SerializeField] ScreenController screenController = default;

    // ポインターの場所を取得する時間
    [SerializeField] float pointerGetTime = 30.0f;

    // 移動制限パラメーター
    [SerializeField] RectTransform movingLimitPoint = default;
    [SerializeField] RectTransform myRectTransform = default;

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
        screenController.SetEndGameAction(PriorityOrder.Slow, ResetOnEndGame);
        startPosition = new Vector2(0, 0);
        endPosition = new Vector2(0, 0);
        speed = 0.0f;
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        if (!playerHitChecker.IsAutoMove)
        {
            MovingLimit();
        }
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
    /// ゲームシーン終了時に行うリセット
    /// </summary>
    void ResetOnEndGame()
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
    /// 瓦に当たり強制的にドラッグが終了したときの処理
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
    /// 移動制限処理
    /// </summary>
    void MovingLimit()
    {
        Vector3 myRectTransformPosition = new Vector3(myRectTransform.transform.position.x, myRectTransform.transform.position.y,0.0f);
        Vector3 moveingLimitTransformPosition = new Vector3(movingLimitPoint.transform.position.x, movingLimitPoint.transform.position.y, 0.0f);
        float limitedX = Mathf.Clamp(myRectTransformPosition.x, -(movingLimitPoint.rect.width * 0.5f) + moveingLimitTransformPosition.x, (movingLimitPoint.rect.width * 0.5f) + moveingLimitTransformPosition.x);
        float limitedY = Mathf.Clamp(myRectTransformPosition.y, -(movingLimitPoint.rect.height * 0.5f) + moveingLimitTransformPosition.y, (movingLimitPoint.rect.height * 0.5f) + moveingLimitTransformPosition.y);
        myRectTransform.transform.position = new Vector3(limitedX, limitedY,0.0f);        
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
