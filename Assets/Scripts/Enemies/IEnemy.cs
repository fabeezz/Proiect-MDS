/// <summary>
/// Interface for enemy behavior. Any enemy that can attack should implement this interface.
/// </summary>
interface IEnemy
{
    /// <summary>
    /// Executes the enemy's attack logic.
    /// </summary>
    public void Attack();
}