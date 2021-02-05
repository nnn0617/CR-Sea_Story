using UnityEngine;
using DG.Tweening;
using ActorsState;

public class PlayerBehaviour : ActorsBehaviour
{
    private GameObject _menuPanel;
    private bool _menuFlag;

    public PlayerBehaviour(int move, int attack):base(move, attack)
    {
        this._moveRange = move;
        this._attackRange = attack;
    }

    void Start()
    {
        _type = UnitType.Player;

        _animator = GetComponent<Animator>();
        _menuPanel = GameObject.FindGameObjectWithTag("Panel");
        _menuFlag = false;

        InitAbility();

        _stateProcessor = new StateProcessor();
        StateIdle = new StateIdle();           //待機状態
        StateSelect = new StateSelect();       //選択状態
        StateMove = new StateMove();           //移動状態
        StateAttack = new StateAttack();       //攻撃状態
        StateIntercept = new StateIntercept(); //傍受状態

        _stateProcessor.State = StateIdle;
        StateIdle.execDelegate = IdleUpdate;
        StateSelect.execDelegate = SelectUpdate;
        StateMove.execDelegate = MoveUpdate;
        StateAttack.execDelegate = AttackUpdate;
        StateIntercept.execDelegate = InterceptUpdate;
    }

    protected override void InitAbility()
    {
        _isMoving = false;
        _isSelecting = false;
        _isRight = 1f;

        _moveRange = 3;
        _attackRange = 1;
        _speed = 8;
        _life = 5;

        _actionVec = new Vector3(0, 0, 0);

        _menuPanel.SetActive(false);
    }

    public override void UnitUpdate()
    {
        if (_stateProcessor.State == null) return;

        _stateProcessor.Execute();//更新処理(状態によって変化)

        //右クリックで選択キャンセル
        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetBool("run", false);
            _stateProcessor.State = StateIdle;
            transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);

            InitAbility();
        }
    }

    public void IdleUpdate()
    {
        transform.position = _startPos;
        //クリックして離した瞬間にSelect_Stateに移行
        if (_isSelecting && Input.GetMouseButtonUp(0))
        {
            _stateProcessor.State = StateSelect;
            _animator.SetBool("run", true);
            transform.DOScale(1.3f, 0.5f).SetEase(Ease.OutElastic);
        }
    }

    public void SelectUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //移動範囲外の場合
            if (CheckDifference(_moveRange)) return;

            _isMoving = true;
            _stateProcessor.State = StateMove;
        }
    }

    public void MoveUpdate()
    {
        MoveToDestination();//ユニットの移動
    }

    public void AttackUpdate()
    {
        transform.DOScale(1.0f, 0.5f).SetEase(Ease.OutElastic);
        if (Input.GetMouseButtonDown(0))
        {
            //攻撃範囲外の場合
            if (CheckDifference(_attackRange)) return;

            _animator.SetBool("attack", true);

            Debug.Log("攻撃");
            _stateProcessor.State = StateIntercept;
            _isSelecting = false;
        }
    }

    public void InterceptUpdate()
    {
        _animator.SetBool("attack", false);
    }

    //マウスカーソルがユニット上にある場合
    private void OnMouseOver()
    {
        //選択状態でその場をクリックした場合はMove_Stateに移行しない
        if (_stateProcessor.State == StateSelect)
        {
            _isMoving = false;
            _stateProcessor.State = StateSelect;
        }
    }

    private void OnMouseDown()
    {
        if (_stateProcessor.State == StateIdle)
        {
            _isSelecting = true;
        }
        if (_stateProcessor.State == StateAttack)
        {
            _isMoving = false;
        }
    }

    //行動範囲チェック(範囲外…true、範囲内…false)
    private bool CheckDifference(int actionRange)
    {
        //マウスカーソルの座標取得
        _diffPos = RoundToPosition(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        _diffPos.z = 0;

        //移動距離の計算
        _startPos = RoundToPosition(transform.position);
        _actionVec = _diffPos - _startPos;

        float range = Mathf.Abs(_actionVec.x) + Mathf.Abs(_actionVec.y);

        if (range > actionRange) return true;

        if (_actionVec.x < 0
            || transform.position.x > Input.mousePosition.x)
        {
            _isRight = -1f;//画像反転
        }
        transform.localScale = new Vector3(_isRight, 1, 1);

        return false;
    }

    //ユニットの移動
    void MoveToDestination()
    {
        if (Mathf.Abs(_diffPos.x) > Mathf.Abs(_diffPos.y))
        {
            HorizontalPriorityMove();
        }
        else if (Mathf.Abs(_diffPos.y) > Mathf.Abs(_diffPos.x))
        {
            VerticalPriorityMove();
        }
        else
        {
            HorizontalPriorityMove();
        }
    }

    //横優先移動
    void HorizontalPriorityMove()
    {
        if (transform.position.x != _diffPos.x)
        {
            Vector3 tmpDiffPos = new Vector3(_diffPos.x, transform.position.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//横移動
        }
        else if (transform.position.y != _diffPos.y)
        {
            Vector3 tmpDiffPos = new Vector3(transform.position.x, _diffPos.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//縦移動
        }
        else
        {
            _actionVec = new Vector3(0, 0, 0);
            _animator.SetBool("run", false);
            _menuPanel.SetActive(true);
            _stateProcessor.State = StateAttack;
        }
    }

    //縦優先移動
    void VerticalPriorityMove()
    {
        if (transform.position.y != _diffPos.y)
        {
            Vector3 tmpDiffPos = new Vector3(transform.position.x, _diffPos.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//縦移動
        }
        else if (transform.position.x != _diffPos.x)
        {
            Vector3 tmpDiffPos = new Vector3(_diffPos.x, transform.position.y, 0.0f);
            transform.position = Vector3.MoveTowards(transform.position, tmpDiffPos, Time.deltaTime * _speed);//横移動
        }
        else
        {
            _actionVec = new Vector3(0, 0, 0);
            _animator.SetBool("run", false);
            _menuPanel.SetActive(true);
            _stateProcessor.State = StateAttack;
        }
    }
}
