using System.Collections.Generic;
using Cysharp.Threading.Tasks;

public class EventsService : IService, IPreparable
{
    private ModifierService ModifierService { get; set; }

    public UniTask Prepare()
    {
        var application = Application.Instance;

        ModifierService = application.GetService<ModifierService>();

        return UniTask.CompletedTask;
    }

/*
    public async UniTask DoGameplayStartEvent()
    {
        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData blessing in ModifierService.Modifiers)
        {
            tasks.Add(blessing.DoBattleStartEvent(TeamType.Player, Battle));
        }

        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoBattleStartEvent(TeamType.Player, Battle));
        }

        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoBattleStartEvent(TeamType.Enemies, Battle));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoBattleStartEvent(TeamType.Enemies, Battle));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoBattleStartEvent(battlePawn.Team, Battle)); 
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTaskVoid DoPawnEnterBattleEvent(Pawn pawn)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoPawnEnterBattleEvent(TeamType.Player, Battle, pawn));
        }

        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoPawnEnterBattleEvent(TeamType.Player, Battle, pawn));
        }

        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoPawnEnterBattleEvent(TeamType.Enemies, Battle, pawn));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoPawnEnterBattleEvent(TeamType.Enemies, Battle, pawn));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoPawnEnterBattleEvent(battlePawn.Team, Battle, pawn));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoBattleFinishedEvent()
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoBattleFinishedEvent(TeamType.Player, Battle));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoBattleFinishedEvent(TeamType.Player, Battle));
        }

        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoBattleFinishedEvent(TeamType.Enemies, Battle));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoBattleFinishedEvent(TeamType.Enemies, Battle));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoBattleFinishedEvent(battlePawn.Team, Battle)); 
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoAttackEvent(Pawn abilityUser, Ability ability)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();
        
        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoAttackEvent(TeamType.Player, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoAttackEvent(TeamType.Player, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoAttackEvent(TeamType.Enemies, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoAttackEvent(TeamType.Enemies, Battle, abilityUser, ability));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoAttackEvent(battlePawn.Team, Battle, abilityUser, ability));
        }

        await UniTask.WhenAll(tasks);
    }

    public async UniTask DoSpecialAttackEvent(Pawn abilityUser, Ability ability)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoSpecialAttackEvent(TeamType.Player, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoSpecialAttackEvent(TeamType.Player, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoSpecialAttackEvent(TeamType.Enemies, Battle, abilityUser, ability));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoSpecialAttackEvent(TeamType.Enemies, Battle, abilityUser, ability));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoSpecialAttackEvent(battlePawn.Team, Battle, abilityUser, ability));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoPawnDeathEvent(Pawn dead, Attack attack)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoPawnDeathEvent(TeamType.Player, Battle, dead, attack));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoPawnDeathEvent(TeamType.Player, Battle, dead, attack));
        }

        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoPawnDeathEvent(TeamType.Enemies, Battle, dead, attack));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoPawnDeathEvent(TeamType.Enemies, Battle, dead, attack));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoPawnDeathEvent(battlePawn.Team, Battle, dead, attack));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoHealthGainedEvent(Pawn pawn, int value)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoHealthGainedEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoHealthGainedEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoHealthGainedEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoHealthGainedEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoHealthGainedEvent(battlePawn.Team, Battle, pawn, value));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoHealthLostEvent(Pawn pawn, Attack attack)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoHealthLostEvent(TeamType.Player, Battle, pawn, attack));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoHealthLostEvent(TeamType.Player, Battle, pawn, attack));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoHealthLostEvent(TeamType.Enemies, Battle, pawn, attack));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoHealthLostEvent(TeamType.Enemies, Battle, pawn, attack));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoHealthLostEvent(battlePawn.Team, Battle, pawn, attack));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoManaGainedEvent(Pawn pawn, int value)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoManaGainedEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoManaGainedEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoManaGainedEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoManaGainedEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoManaGainedEvent(battlePawn.Team, Battle, pawn, value));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTask DoManaLostEvent(Pawn pawn, int value)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoManaLostEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoManaLostEvent(TeamType.Player, Battle, pawn, value));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoManaLostEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoManaLostEvent(TeamType.Enemies, Battle, pawn, value));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoManaLostEvent(battlePawn.Team, Battle, pawn, value));
        }

        await UniTask.WhenAll(tasks);
    }
    
    public async UniTaskVoid DoBuffReceivedEvent(Buff buff)
    {
        if (Battle == null)
            return;

        var tasks = new List<UniTask>();

        foreach (IBaseEventData blessing in BlessingService.Blessings)
        {
            tasks.Add(blessing.DoBuffReceivedEvent(TeamType.Player, Battle, buff));
        }
        
        foreach (IBaseEventData synergy in SynergyService.Synergies)
        {
            tasks.Add(synergy.DoBuffReceivedEvent(TeamType.Player, Battle, buff));
        }
        
        foreach (IBaseEventData bossBlessing in Battle.CombatEncounterBlessings)
        {
            tasks.Add(bossBlessing.DoBuffReceivedEvent(TeamType.Enemies, Battle, buff));
        }
        
        foreach (IBaseEventData synergy in Battle.CombatEncounterSynergies)
        {
            tasks.Add(synergy.DoBuffReceivedEvent(TeamType.Enemies, Battle, buff));
        }
        
        foreach (var battlePawn in Battle.Pawns)
        {
            tasks.Add(((IBaseEventData)battlePawn).DoBuffReceivedEvent(battlePawn.Team, Battle, buff));
        }
        
        await UniTask.WhenAll(tasks);
    }
    */
}