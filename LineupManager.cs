using UnityEngine;
using UnityEngine.UI;

// Muestra titulares y suplentes. Asigna los campos UI desde el Inspector de Unity.
public class LineupManager : MonoBehaviour {
 public RosterDatabase database; public Transform listRoot; public Text rowPrefab; public Text teamTitle; public Text formationLabel;
 void Start(){ if(database==null) database=FindObjectOfType<RosterDatabase>(); if(database!=null) database.LoadRosters(); ShowRealMadrid(); }
 public void ShowRealMadrid(){ ShowTeam("real_madrid"); } public void ShowBarcelona(){ ShowTeam("barcelona"); }
 public void ShowTeam(string id){ if(database==null||listRoot==null||rowPrefab==null){Debug.LogWarning("Asigna Database, List Root y Row Prefab.");return;} var t=database.GetTeam(id); if(t==null)return; if(teamTitle!=null)teamTitle.text=t.name; if(formationLabel!=null)formationLabel.text="Formación sugerida: "+t.formation; foreach(Transform c in listRoot)Destroy(c.gameObject); AddRow("TITULARES",true); foreach(var p in t.players)if(p.starter)AddPlayer(p); AddRow("SUPLENTES",true); foreach(var p in t.players)if(!p.starter)AddPlayer(p); }
 void AddPlayer(RosterPlayer p){string pos=p.position=="GK"?"Portero":p.position=="DF"?"Defensa":p.position=="MF"?"Centrocampista":"Delantero"; AddRow(p.number.ToString("00")+"  "+p.name+"  ·  "+pos,false);}
 void AddRow(string s,bool header){var row=Instantiate(rowPrefab,listRoot);row.text=s;row.fontStyle=header?FontStyle.Bold:FontStyle.Normal;}
}
