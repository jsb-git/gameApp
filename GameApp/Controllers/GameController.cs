using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using GameApp.Models;
using PagedList;

namespace GameApp.Controllers
{
    public class GameController : Controller
    {

        static IList<Game> gameList = new List<Game>
        {
            new Game() {GameId = 1, Name="Witcher 3", Genre="Action RPG", Platform="PC", dateBought=new DateTime(2015,12,25), hoursPlayed=170, GameStatus=Status.Completed,  rating=5},
            new Game() {GameId = 2, Name="Borderlands", Genre="Looter Shooter", Platform="PC", dateBought=new DateTime(2007,3,12,10,30,50), hoursPlayed=80, GameStatus=Status.Not_played_yet, rating=4},
            new Game() {GameId = 3, Name="Destiny", Genre="Looter Shooter", Platform="PC", dateBought=new DateTime(2012,3,11), hoursPlayed=50, GameStatus=Status.New_Game_plus, rating=4},
            new Game() {GameId = 4, Name="Bloodborne", Genre="SoulsBorne", Platform="PS4", dateBought=new DateTime(2015,7,15), hoursPlayed=85, GameStatus=Status.Completed, rating=5},
            new Game() {GameId = 5, Name="Hollow Knight", Genre="MetroidVania", Platform="PC", dateBought=new DateTime(2017,11,8), hoursPlayed=70, GameStatus=Status.New_Game_plus, rating=5},
            new Game() {GameId = 6, Name="Hades", Genre="MetroidVania", Platform="PC", dateBought=new DateTime(2019,2,10), hoursPlayed=50, GameStatus=Status.Completed, rating=5},
            new Game() {GameId = 7, Name="Slay the Spire", Genre="Card Game Indie", Platform="PC", dateBought=new DateTime(2019,4,9), hoursPlayed=60, GameStatus=Status.Completed, rating=5}
        };


        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.HourSortParm = sortOrder == "hours_played" ? "hours_played_desc" : "hours_played";
            ViewBag.GenreSortParm = sortOrder == "genre" ? "genre_desc" : "genre";
            
            
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var list = from s in gameList
                       select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                list = list.Where(s => s.Name.ToLower().Contains(searchString.ToLower()) || s.Genre.ToLower().Contains(searchString.ToLower()));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    list = list.OrderByDescending(s => s.Name);
                    break;     
                case "genre_desc":
                    list = list.OrderByDescending(s => s.Genre);
                    break;
                case "genre":
                    list = list.OrderBy(s => s.Genre);
                    break;
                case "hours_played":
                    list = list.OrderBy(s => s.hoursPlayed);
                    break;
                case "hours_played_desc":
                    list = list.OrderByDescending(s => s.hoursPlayed);
                    break;
                default:
                    list = list.OrderBy(s => s.Name);
                    break;
            }

            int pageSize = 3;
            int pageNumber = (page ?? 1);
            return View(list.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Edit(int id)
        {
            var game = gameList.Where(s => s.GameId == id).FirstOrDefault();
            return View(game);
        }

        [HttpPost]
        public ActionResult Edit(Game selectedGame)
        {
            if (ModelState.IsValid)
            {
                var filteredGame = gameList.Where(s => s.GameId == selectedGame.GameId).FirstOrDefault();
                gameList.Remove(filteredGame);
                gameList.Add(selectedGame);
                return RedirectToAction("Index");
            }
            return View(selectedGame);
            
        }

        public ActionResult Delete(int id)
        {
            var filteredgame = gameList.Where(s => s.GameId == id).FirstOrDefault();
            return View(filteredgame);
        }

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var filteredGame = gameList.Where(s => s.GameId == id).FirstOrDefault();
            gameList.Remove(filteredGame);
            return RedirectToAction("Index");
        } 

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Game insertedGame)
        {
            if (ModelState.IsValid)
            {
                int maxId = (from s in gameList
                           select s.GameId).Max();
                //Increment gameId
                insertedGame.GameId = maxId + 1;
                gameList.Add(insertedGame);
                return RedirectToAction("Index");
            }
            return View(insertedGame);
        }

        public ActionResult Details(int id)
        {
            var detailresult = gameList.Where(s => s.GameId == id).FirstOrDefault();
            return View(detailresult);
        }

        

        
    }
}