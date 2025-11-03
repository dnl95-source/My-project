using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    public Dropdown raceDropdown, sexDropdown, kingdomDropdown, roleDropdown;
    public InputField nameInput;
    public GameObject menuPanel;

    public void OnStartButton()
    {
        var race = (RaceType)raceDropdown.value;
        var sex = (Sex)sexDropdown.value;
        var kingdom = (Kingdom)kingdomDropdown.value;
        var name = nameInput.text;
        var charAttr = CharacterGenerator.GenerateCharacter(race, sex, kingdom, name);
        GameManager.Instance.StartGame(charAttr);
        menuPanel.SetActive(false);
    }
}