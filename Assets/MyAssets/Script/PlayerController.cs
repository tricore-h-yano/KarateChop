using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TouchScript.Gestures.TransformGestures;
using TouchScript.Behaviors;


public class PlayerController : MonoBehaviour
{
    // ジェスチャークラス
    [SerializeField] ScreenTransformGesture screenTransformGesture = default;

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
        startPosition = new Vector2(0, 0);
        endPosition = new Vector2(0, 0);
    }

    /// <summary>
    /// 更新処理
    /// </summary>
    void Update()
    {
        ++time;
    }

    /// <summary>
    /// 有効化されたときの処理
    /// </summary>
    void OnEnable()
    {
        // Transform Gestureのdelegateに登録
        screenTransformGesture.TransformStarted += TransformStartedHandle; // 変形開始
        screenTransformGesture.StateChanged += StateChangedHandle; //　状態変化
        screenTransformGesture.TransformCompleted += TransformCompletedHandle; // 変形終了
        screenTransformGesture.Cancelled += CancelledHandle; // キャンセル
    }

    /// <summary>
    /// 無効化されたときの処理
    /// </summary>
    void OnDisable()
    {
        UnsubscribeEvent();
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
        screenTransformGesture.TransformStarted -= TransformStartedHandle;
        screenTransformGesture.StateChanged -= StateChangedHandle;
        screenTransformGesture.TransformCompleted -= TransformCompletedHandle;
        screenTransformGesture.Cancelled -= CancelledHandle;
    }

    /// <summary>
    /// 変形開始時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TransformStartedHandle(object sender, System.EventArgs e)
    {
    }

    /// <summary>
    /// 変形中の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void StateChangedHandle(object sender, System.EventArgs e)
    {
        if(time >= 30)
        {
            startPosition = screenTransformGesture.ScreenPosition;
            time = 0;
        }

    }

    /// <summary>
    /// 変形終了時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void TransformCompletedHandle(object sender, System.EventArgs e)
    {

    }

    /// <summary>
    /// キャンセルされた時の処理
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void CancelledHandle(object sender, System.EventArgs e)
    {
        endPosition = screenTransformGesture.ScreenPosition;
        SpeedCalculation();
    }

    /// <summary>
    /// 速度計算処理
    /// </summary>
    void SpeedCalculation()
    {
        float swipeLength = endPosition.y - startPosition.y;

        //speed = swipeLength * -1;
        speed = Mathf.Abs(swipeLength);
        Debug.Log(speed);

        if(speed <= 5.0f)
        {
            speed = 5.0f;
        }
    }


}
