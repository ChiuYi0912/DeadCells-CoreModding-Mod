using dc;
using dc.en;
using dc.libs.heaps;
using dc.pow;
using dc.pr;
using dc.tool;
using dc.tool.weap;
using dc.ui.icon;
using HaxeProxy.Runtime;
using Kaguya;
using ModCore.Events.Interfaces.Game;
using ModCore.Storage;
using ModCore.Utitities;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

internal class OtherDashSword(Hero o, InventItem i) : DashSword(o, i), IHxbitSerializable<object>, IOnGameExit
{
    #region 常量和字段
    public static string name = "OtherDashSword";
    private static readonly string[] _weaponNames = ["Other_QueenRapier"];
    private Weapon[]? _weapons;
    private int _currentWeap;
    private Other_QueenRapier? _cachedQueenRapier;
    private bool _isInitialized = false;
    #endregion

    #region 序列化接口
    object IHxbitSerializable<object>.GetData() => new();
    void IHxbitSerializable<object>.SetData(object data) { }
    #endregion

    #region 初始化方法
    [MemberNotNull(nameof(_weapons))]
    private void InitializeWeapons()
    {
        if (_weapons != null) return;

        _weapons = new Weapon[_weaponNames.Length];
        for (int i = 0; i < _weapons.Length; i++)
        {
            var name = _weaponNames[i];
            var item = new InventItem(new InventItemKind.Weapon(name.AsHaxeString()))
            {
                _itemLevel = this.item._itemLevel
            };
            item.setItemLevel(this.item.getRawItemLevel());

            _weapons[i] = Weapon.Class.create(owner, item);
        }

        _isInitialized = true;
    }

    private Other_QueenRapier GetOrCreateQueenRapier()
    {
        if (_cachedQueenRapier == null || _cachedQueenRapier.destroyed)
        {
            var hero = ModCore.Modules.Game.Instance.HeroInstance;
            if (hero != null)
            {
                _cachedQueenRapier = new Other_QueenRapier(hero, item);
            }
        }
        return _cachedQueenRapier!;
    }
    #endregion

    #region 公共方法重写
    public override bool onExecute()
    {
        base.onExecute();
        InitializeWeapons();

        _currentWeap = (_currentWeap + 1) % _weapons.Length;
        var currentWeapon = _weapons[_currentWeap];

        Entity? targetEntity = this.owner as Entity;
        if (targetEntity == null) return false;

        var queen = GetOrCreateQueenRapier();
        if (queen != null)
        {
            queen.queenStrike(targetEntity, 0, 0, 0);
        }

        currentWeapon.onLevelChanged(owner._level);

        return isLastCycle() ? currentWeapon.onExecute() : base.onExecute();
    }

    public override void hitFromWeapon(Entity e, Ref<int> _cycle)
    {
        InitializeWeapons();
        if (_currentWeap >= 0 && _currentWeap < _weapons.Length)
        {
            _weapons[_currentWeap].hitFromWeapon(e, _cycle);
        }

        base.hitFromWeapon(e, _cycle);
    }

    public override void fixedUpdate()
    {
        base.fixedUpdate();

        if (_weapons != null)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                _weapons[i].fixedUpdate();
            }
        }
    }

    // public override void onLevelChanged(Level lvl)
    // {
    //     InitializeWeapons();
    //     base.onLevelChanged(lvl);
    //     if (_weapons != null)
    //     {
    //         for (int i = 0; i < _weapons.Length; i++)
    //         {
    //             _weapons[i].onLevelChanged(lvl);
    //         }
    //     }
    // }
    #endregion

    #region 资源管理
    public override void dispose()
    {
        if (_weapons != null)
        {
            for (int i = 0; i < _weapons.Length; i++)
            {
                _weapons[i]?.dispose();
            }
            _weapons = null;
        }

        _cachedQueenRapier?.dispose();
        _cachedQueenRapier = null;
        _isInitialized = false;

        base.dispose();
    }
    #endregion

    #region 工具方法
    private bool IsValidIndex(int index)
    {
        return _weapons != null && index >= 0 && index < _weapons.Length;
    }

    #region 接口实现
    void IOnGameExit.OnGameExit()
    {

    }

    #endregion
}



#endregion