namespace Weapons {
    public interface IWeaponBehavior
    {
        public void EnterState(Player player, WeaponSO weapon);
        public void ExitState(Player player, WeaponSO weapon);
        public void UpdateState();
    }
}
