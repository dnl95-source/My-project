// Wrapper globale per compatibilità: inoltra a Game.Characters.CharacterGenerator
public static class CharacterGenerator
{
    public static CharacterAttributes GenerateCharacter()
    {
        return Game.Characters.CharacterGenerator.GenerateCharacter();
    }

    public static CharacterAttributes GenerateCharacter(string name)
    {
        return Game.Characters.CharacterGenerator.GenerateCharacter(name);
    }

    public static CharacterAttributes GenerateCharacter(string name, Game.Combat.RaceType race)
    {
        return Game.Characters.CharacterGenerator.GenerateCharacter(name, race);
    }

    public static CharacterAttributes GenerateCharacter(string name, Game.Combat.RaceType race, int level)
    {
        return Game.Characters.CharacterGenerator.GenerateCharacter(name, race, level);
    }

    public static CharacterAttributes GenerateCharacter(params object[] args)
    {
        return Game.Characters.CharacterGenerator.GenerateCharacter(args);
    }
}