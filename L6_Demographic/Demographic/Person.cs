using Demographic.Tables;

namespace Demographic;

public class Person
{
    public Genders Gender { get; }
    public int BirthYear { get; }

    public Person? Couple { get; private set; }

    public bool SuitableRelation()
    {
        return Age >= 18 && Couple == null;
    }

    public bool SuitablePartner(Person person)
    {
        return person.Gender != Gender && Math.Abs(person.Age - Age) <= 5;
    }

    public bool Engaged => Couple != null;

    public void Engage(Person person)
    {
        person.Couple = this;
        Couple = person;
    }

    public void Disengage()
    {
        if (Couple == null) return;
        Couple.Couple = null;
        Couple = null;
    }

    public int Age { get; private set; }
    
    public int DeathYear { get; private set; }

    public delegate void ChildBirthHandler(Genders gender);
    public event ChildBirthHandler? ChildBirth;

    public delegate void PersonDeathHandler(Person person);
    public event PersonDeathHandler? PersonDeath;

    public delegate void FindPartnerHandler(Person person);
    public event FindPartnerHandler? FindPartner;
    
    public delegate void LosePartnerHandler(Person person);
    public event LosePartnerHandler? LosePartner;

    public Person(Genders gender, int birthYear, int age)
    {
        Gender = gender;
        BirthYear = birthYear;
        Couple = null;
        Age = age;
        DeathYear = 0;
    }
    
    private static class ProbabilityCalculator
    {
        private static readonly Random Random = new();

        public static bool IsEventHappened(double eventProbability)
        {
            return Random.NextDouble() <= eventProbability;
        }

        public static Genders DetermineChildGender()
        {
            return IsEventHappened(0.55) ? Genders.Female : Genders.Male;
        }
    }

    public void YearTickHandler(DeathRulesTable deathRulesTable, RelationshipsRulesTable relationshipsRulesTable)
    {
        Age++;

        if (SuitableRelation() &&
            ProbabilityCalculator.IsEventHappened(relationshipsRulesTable.GetFindPartnerProbability(Age)))
        {
            FindPartner?.Invoke(this);
        }

        if (Gender == Genders.Female && Engaged &&
            Age is >= 18 and <= 45 &&
            ProbabilityCalculator.IsEventHappened(0.151))
        {
            var gender = ProbabilityCalculator.DetermineChildGender();
            ChildBirth?.Invoke(gender);
        }
        
        if (Engaged &&
            ProbabilityCalculator.IsEventHappened(relationshipsRulesTable.GetLosePartnerProbability(Age)))
        {
            LosePartner?.Invoke(this);
        }
        
        if (ProbabilityCalculator.IsEventHappened(deathRulesTable.GetDeathProbability(Gender, Age)))
        {
            PersonDeath?.Invoke(this);
            DeathYear = BirthYear + Age;
        }
    }
}
