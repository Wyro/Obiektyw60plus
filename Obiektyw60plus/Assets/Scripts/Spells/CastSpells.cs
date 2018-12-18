using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastSpells : MonoBehaviour {

    Spell spell;

    private int spellNumber;

    public List<Spell> spellsList = new List<Spell>();
                                      
	void Start () {
        spellNumber = 0;

        spell = (Spell)Resources.Load("Spells/" + gameObject.name, typeof(Spell));

        List<Spell> spellDatabase = GameObject.Find("SpellManager").GetComponent<SpellManager>().spellList;
        
        foreach(Spell spell_ in spellDatabase)
        {
            spellsList.Add(spell_);
        }
	}

    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Z) && spellNumber != spellsList.Count)
        {
            spellNumber++;
        }
        else if (Input.GetKeyDown(KeyCode.Z) && spellNumber == spellsList.Count)
        {
            spellNumber = 0;
        }

        if (Input.GetKeyDown(KeyCode.K))
        {
            CastMagic(spellsList[spellNumber]);
            if (spell)
                CastMagic(spell);
        }
    }

    void CastMagic(Spell spell)
    {
        if (!spell.spellPrefab)
        {
            Debug.LogWarning("No prefab assigned to the spell!");
            return;
        }
        else
        {
            GameObject spellObject = Instantiate(spell.spellPrefab, GameObject.FindGameObjectWithTag("MagicSpawn").GetComponent<Transform>().position, Camera.main.GetComponent<Transform>().rotation);
            spellObject.AddComponent<Rigidbody>();
            spellObject.GetComponent<Rigidbody>().useGravity = false;
            spellObject.GetComponent<Rigidbody>().velocity = spellObject.transform.forward * spell.projectableSpeed;
            spellObject.name = spell.spellName;
            spellObject.transform.parent = GameObject.Find("SpellManager").transform;

            Destroy(spellObject, 2);
        }
    }
	
	
}
