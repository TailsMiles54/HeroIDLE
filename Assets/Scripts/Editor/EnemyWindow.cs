using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyWindow : EditorWindow
{
    private VisualElement m_RightPane;
    
    public void CreateGUI()
    {
        // Get a list of all sprites in the project
        var allObjectGuids = AssetDatabase.FindAssets("t:EnemySetting");
        var allObjects = new List<EnemySetting>();
        foreach (var guid in allObjectGuids)
        {
            allObjects.Add(AssetDatabase.LoadAssetAtPath<EnemySetting>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        // Create a two-pane view with the left pane being fixed with
        var splitView = new TwoPaneSplitView(0, 250, TwoPaneSplitViewOrientation.Horizontal);

        // Add the view to the visual tree by adding it as a child to the root element
        rootVisualElement.Add(splitView);

        // A TwoPaneSplitView always needs exactly two child elements
        var leftPane = new ListView();
        splitView.Add(leftPane);
        
        leftPane.makeItem = () => new Label();
        leftPane.bindItem = (item, index) => { (item as Label).text = allObjects[index].name; };
        leftPane.itemsSource = allObjects;
        
        leftPane.onSelectionChange += OnSpriteSelectionChange;
        
        Debug.Log("4");
        m_RightPane = new VisualElement();
        splitView.Add(m_RightPane);
    }

    private void OnSpriteSelectionChange(IEnumerable<object> selectedItems)
    {
        // Clear all previous content from the pane
        m_RightPane.Clear();

        // Get the selected sprite
        var selectedEnemy = selectedItems.First() as EnemySetting;
        if (selectedEnemy == null)
            return;

        Color nameColor = selectedEnemy.Type == EnemySetting.EnemyType.Fishman ? Color.red : Color.green;  
        var nameText = new Label(selectedEnemy.Name)
        {
            style =
            {
                fontSize = 16,
                unityFontStyleAndWeight = FontStyle.Bold,
                unityTextAlign = TextAnchor.MiddleCenter,
                color = nameColor
            }
        };
        var damage = new TextField("Damage")
        {
            style =
            {
                fontSize = 14,
                unityTextAlign = TextAnchor.MiddleCenter,
            }
        };
        var autoAttackTime = new TextField("AutoAttackTime")
        {
            style =
            {
                fontSize = 14,
                unityTextAlign = TextAnchor.MiddleCenter,
            },
        };
        autoAttackTime.RegisterCallback<ChangeEvent<string>>((evt) =>
        {
            var newValue = evt.newValue;
            selectedEnemy.AutoAttackTimeSet(float.Parse(newValue));
        });
        
        var enemyType = new EnumField(selectedEnemy.Type)
        {
            style =
            {
                fontSize = 14,
                unityTextAlign = TextAnchor.MiddleCenter,
            }
        };
            
        // Add the Image control to the right-hand pane
        m_RightPane.Add(nameText);
        m_RightPane.Add(damage);
        m_RightPane.Add(autoAttackTime);
        m_RightPane.Add(enemyType);
    }
    
    [MenuItem("HeroIDLE/Enemy Editor")]
    public static void ShowMyEditor()
    {
        // This method is called when the user selects the menu item in the Editor
        EditorWindow wnd = GetWindow<EnemyWindow>();
        wnd.titleContent = new GUIContent("Enemy Editor");
    }
}