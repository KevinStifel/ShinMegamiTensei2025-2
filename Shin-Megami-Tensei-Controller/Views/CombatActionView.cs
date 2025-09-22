using Shin_Megami_Tensei_View;

namespace Shin_Megami_Tensei;

public class CombatActionView : AbstractView
{
    public CombatActionView(View view) : base(view) { }

    public void ShowAvailableTargets(UnitBase attacker, List<UnitBase> enemies)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Seleccione un objetivo para {attacker.Name}");
        for (int i = 0; i < enemies.Count; i++)
        {
            var target = enemies[i];
            View.WriteLine($"{i + 1}-{target.Name} HP:{target.Stats.HP}/{target.Stats.MaxHP} MP:{target.Stats.MP}/{target.Stats.MaxMP}");
        }
        View.WriteLine($"{enemies.Count + 1}-Cancelar");
    }
    
    public int ReadEnemyTargetIndex(UnitBase attacker, List<UnitBase> aliveEnemies)
    {
        ShowAvailableTargets(attacker, aliveEnemies);
        string menuSelection = ReadUserSelection();
        int index = int.Parse(menuSelection) - 1;
        return index;
    }

    public void ShowAttackResult(UnitBase attacker, UnitBase target, int damage)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"{attacker.Name} ataca a {target.Name}");
        View.WriteLine($"{target.Name} recibe {damage} de daño");
        View.WriteLine($"{target.Name} termina con HP:{target.Stats.HP}/{target.Stats.MaxHP}");
    }
    public void ShowTurnConsumption(int consumedFull, int consumedBlinking, int gainedBlinking)
    {
        View.WriteLine("----------------------------------------");
        View.WriteLine($"Se han consumido {consumedFull} Full Turn(s) y {consumedBlinking} Blinking Turn(s)");
        View.WriteLine($"Se han obtenido {gainedBlinking} Blinking Turn(s)");
    }
}