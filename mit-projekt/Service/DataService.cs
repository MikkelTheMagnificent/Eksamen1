using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Sqlite;
using System.Text.Json;

using Data;
using Models;

namespace Service;

public class DataService
{
    private dbContext db { get; }

    public DataService(dbContext db) {
        this.db = db;
    }
 public void SeedData() {
        
        Kunde kunde = db.Kunder.FirstOrDefault()!;
        if (kunde == null) {
            kunde = new Kunde { Navn = "Dan", Email = "dan@dandan.dk", Type = "Erhverv", Id = 1 };
            db.Kunder.Add(new Kunde {  Navn = "Thomas", Email = "thom@thom.dk", Type = "Erhverv", Id = 2 });
            db.Kunder.Add(new Kunde {  Navn = "Bo", Email = "bo@bobo.dk", Type = "Privat", Id = 3 });
        db.SaveChanges();
        }
    }


     public List<Kunde> GetKunder() {
        return db.Kunder
            .ToList();
    }

    public Kunde GetKundeById(int id) {
        var kunde = db.Kunder
        .Where(k => k.Id == id)
        .FirstOrDefault();
        return kunde;
    }

     public void CreateKunde(Kunde kunde){
            db.Kunder.Add(new Kunde{Navn = kunde.Navn, Email = kunde.Email, Type = kunde.Type});
            db.SaveChanges();
           
        }
}