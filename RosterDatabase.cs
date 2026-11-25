using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable] public class RosterPlayer { public string name; public int number; public string position; public bool starter; }
[Serializable] public class RosterTeam { public string id; public string name; public string formation; public List<RosterPlayer> players = new List<RosterPlayer>(); }
[Serializable] public class RosterFile { public string season; public string lastUpdated; public List<RosterTeam> teams = new List<RosterTeam>(); public string[] sources; public string note; }

// Carga los datos de Assets/StreamingAssets/team_rosters_2026_27.json
public class RosterDatabase : MonoBehaviour {
 public RosterFile Data { get; private set; }
 public void LoadRosters() {
  string path = System.IO.Path.Combine(Application.streamingAssetsPath, "team_rosters_2026_27.json");
  if (!System.IO.File.Exists(path)) { Debug.LogError("No se encontró el JSON de plantillas: "+path); return; }
  Data = JsonUtility.FromJson<RosterFile>(System.IO.File.ReadAllText(path));
 }
 public RosterTeam GetTeam(string id) { if (Data == null) LoadRosters(); return Data == null ? null : Data.teams.Find(t => t.id == id); }
 public List<RosterPlayer> GetStarters(string id) { var t=GetTeam(id); return t==null?new List<RosterPlayer>():t.players.FindAll(p=>p.starter); }
 public List<RosterPlayer> GetSubstitutes(string id) { var t=GetTeam(id); return t==null?new List<RosterPlayer>():t.players.FindAll(p=>!p.starter); }
}
