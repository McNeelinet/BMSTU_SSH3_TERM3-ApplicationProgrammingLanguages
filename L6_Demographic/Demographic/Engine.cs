using Demographic.Structs;
using Demographic.Tables;

namespace Demographic;

public class Engine : IEngine
{
    private readonly List<Person> _persons;
    private readonly DeathRulesTable _deathRulesTable;
    private readonly RelationshipsRulesTable _relationshipsRulesTable;

    public delegate void YearTickHandler(DeathRulesTable deathRules, RelationshipsRulesTable relationshipsRulesTable);
    public event YearTickHandler? YearTick;
    
    public List<YearCount> MaleDynamic { get; private set; }
    public List<YearCount> FemaleDynamic { get; private set; }

    public List<AgeCount> MaleAgeDistribution
    {
        get
        {
            return _persons
                .Where(person => person.Gender == Genders.Male)
                .GroupBy(person => person.Age)
                .Select(group => new AgeCount(group.Key, group.Count()))
                .ToList();
        }
    }
    
    public List<AgeCount> FemaleAgeDistribution
    {
        get
        {
            return _persons
                .Where(person => person.Gender == Genders.Female)
                .GroupBy(person => person.Age)
                .Select(group => new AgeCount(group.Key, group.Count()))
                .ToList();
        }
    }

    private int CurrentYear { get; set; }
    private int EndYear { get; }

    public Engine(DeathRulesTable deathRulesTable, RelationshipsRulesTable relationshipsRulesTable, int currentYear, int endYear)
    {
        _deathRulesTable = deathRulesTable;
        _relationshipsRulesTable = relationshipsRulesTable;
        _persons = new List<Person>();

        MaleDynamic = new List<YearCount>();
        FemaleDynamic = new List<YearCount>();
        
        CurrentYear = currentYear;
        EndYear = endYear;
    }

    public void CreatePopulation(InitialAgeTable initialAgeTable, int population)
    {
        _persons.Clear();
        MaleDynamic.Clear();
        FemaleDynamic.Clear();
        
        foreach (var group in initialAgeTable.GetDistribution(population / 1000))
        {
            for (var i = 0; i < group.Count; ++i)
            {
                var person = new Person((Genders)(_persons.Count % 2), CurrentYear - group.Age, group.Age);
                AddPerson(person);
            }
        }
        
        
    }

    public void Execute()
    {
        MaleDynamic.Clear();
        FemaleDynamic.Clear();

        while (CurrentYear < EndYear)
        {
            UpdateDynamic();
            YearTick?.Invoke(_deathRulesTable, _relationshipsRulesTable);
            CurrentYear += 1;
        }
        
        UpdateDynamic();
    }

    private void UpdateDynamic()
    {
        MaleDynamic.Add(
            new YearCount(
                CurrentYear,
                _persons.Count(person => person.Gender == Genders.Male)
            )
        );
        FemaleDynamic.Add(
            new YearCount(
                CurrentYear,
                _persons.Count(person => person.Gender == Genders.Female)
            )
        );
    }

    private void AddPerson(Person person)
    {
        person.ChildBirth += ChildBirthHandler;
        person.PersonDeath += PersonDeathHandler;
        person.FindPartner += FindPartnerHandler;
        person.LosePartner += LosePartnerHandler;
        YearTick += person.YearTickHandler;
                
        _persons.Add(person);
    }

    private void RemovePerson(Person person)
    {
        person.ChildBirth -= ChildBirthHandler;
        person.PersonDeath -= PersonDeathHandler;
        person.FindPartner -= FindPartnerHandler;
        person.LosePartner -= LosePartnerHandler;
        YearTick -= person.YearTickHandler;
        
        _persons.Remove(person); 
    }

    private void ChildBirthHandler(Genders gender)
    {
        var person = new Person(gender, CurrentYear, 0);
        AddPerson(person);
    }

    private void PersonDeathHandler(Person person)
    {
        RemovePerson(person);
    }

    private void FindPartnerHandler(Person person)
    {
        foreach (var candidate in _persons)
        {
            if (person.SuitablePartner(candidate) &&
                candidate.SuitableRelation())
            {
                person.Engage(candidate);
                break;
            }
        }
    }

    private void LosePartnerHandler(Person person)
    {
        person.Disengage();
    }
}
