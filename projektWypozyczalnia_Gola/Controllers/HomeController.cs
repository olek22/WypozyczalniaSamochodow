using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using projektWypozyczalnia_Gola.Models;


namespace projektWypozyczalnia_Gola.Controllers
{
    public class HomeController : Controller
    {
        private WypAutEntities _db = new WypAutEntities();
        // GET: Home
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View(_db.Users.ToList());
        }

        #region // Wypozyczalnia
        [Authorize]
        [HttpGet]
        public ActionResult Wypozyczalnia(string searching, string sorting)
        {
            ViewBag.ulica = sorting == "ulica" ? "ulica desc" : "ulica";
            ViewBag.miasto = sorting == "miasto" ? "miasto desc" : "miasto";
            ViewBag.kod = sorting == "kod" ? "kod desc" : "kod";
            ViewBag.nazwa = sorting == "nazwa" ? "nazwa desc" : "nazwa";

            var widok = _db.wypozyczalnias.Where(x => x.ulica.StartsWith(searching) || x.miasto.StartsWith(searching)
        || x.kod.StartsWith(searching) || x.nazwa.ToString().StartsWith(searching) ||
        x.numer.ToString().StartsWith(searching) || searching == null);

            switch (sorting)
            {
                case "ulica":
                    widok = widok.OrderBy(x => x.ulica);
                    return View(widok.ToList());
                case "ulica desc":
                    widok = widok.OrderByDescending(x => x.ulica);
                    return View(widok.ToList());
                case "miasto desc":
                    widok = widok.OrderByDescending(x => x.miasto);
                    return View(widok.ToList());
                case "miasto":
                    widok = widok.OrderBy(x => x.miasto);
                    return View(widok.ToList());
                case "kod desc":
                    widok = widok.OrderByDescending(x => x.kod);
                    return View(widok.ToList());
                case "kod":
                    widok = widok.OrderBy(x => x.kod);
                    return View(widok.ToList());
                case "nazwa desc":
                    widok = widok.OrderByDescending(x => x.nazwa);
                    return View(widok.ToList());
                case "nazwa":
                    widok = widok.OrderBy(x => x.nazwa);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());
            }
        }

        #region // CreateWypozyczalnia
        public ActionResult CreateWypozyczalnia()
        {
            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateWypozyczalnia(wypozyczalnia newWypozyczalnia)
        {
            try
            {
                _db.wypozyczalnias.Add(newWypozyczalnia);
                _db.SaveChanges();
                return RedirectToAction("Wypozyczalnia");
            }
            catch
            {
                return View(newWypozyczalnia);
            }
        }
        #endregion

        #region // EditWypozyczalnia
        public ActionResult EditWypozyczalnia(int id)
        {
            var wypozyczalniaToEdit = _db.wypozyczalnias.Find(id);
            return View(wypozyczalniaToEdit);
        }
        [HttpPost]
        public ActionResult EditWypozyczalnia(wypozyczalnia wypozyczalniaToEdit)
        {
            var orginalWypozyczalnia = _db.wypozyczalnias.Find(wypozyczalniaToEdit.id);
            try
            {
               if(TryUpdateModel(orginalWypozyczalnia,
                   new string[] { "numer", "ulica", "miasto", "kod", "nazwa" } ))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Wypozyczalnia");
            }
            catch
            {
                return View(orginalWypozyczalnia);
            }
        }
        #endregion

        #region // DeleteWypozyczalnia
        public ActionResult DeleteWypozyczalnia(int id)
        {
            var wypozyczalniaToDelete = _db.wypozyczalnias.Find(id);
            return View(wypozyczalniaToDelete);
        }
        [HttpPost]
        public ActionResult DeleteWypozyczalnia(wypozyczalnia wypozyczalniaToDelete)
        {
            try
            {
                var orginalWypozyczalnia = _db.wypozyczalnias.Find(wypozyczalniaToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalWypozyczalnia);
                }
                _db.wypozyczalnias.Remove(orginalWypozyczalnia);
                _db.SaveChanges();
                return RedirectToAction("Wypozyczalnia");
            }
            catch
            {
                return View(wypozyczalniaToDelete);
            }
        }
        #endregion

        #region // DetailsWypozyczalnia
        public ActionResult DetailsWypozyczalnia(int id)
        {
            var wypozyczalniaToDetails = _db.wypozyczalnias.Find(id);
            return View(wypozyczalniaToDetails);
        }
        #endregion
        #endregion

        #region //Pracownik
        [Authorize]
        [HttpGet]
        public ActionResult Pracownik(string searching, string sorting)
        {
            ViewBag.nazwisko = sorting == "nazwisko" ? "nazwisko desc" : "nazwisko";
            ViewBag.data_zatr = sorting == "data_zatr" ? "data_zatr desc" : "data_zatr";
            ViewBag.miasto = sorting == "miasto" ? "miasto desc" : "miasto";

            var widok = (_db.pracowniks.Where(x => x.imie.StartsWith(searching) || x.kod.StartsWith(searching) ||
            x.nazwisko.StartsWith(searching) || x.miasto.StartsWith(searching) || x.ulica.StartsWith(searching) ||
            x.nr_telefonu.StartsWith(searching) || x.wypozyczalnia.nazwa.StartsWith(searching) || searching == null));

            switch (sorting)
            {
                case "nazwisko":
                    widok = widok.OrderBy(x => x.nazwisko);
                    return View(widok.ToList());
                case "nazwisko desc":
                    widok = widok.OrderByDescending(x => x.nazwisko);
                    return View(widok.ToList());
                case "data_zatr desc":
                    widok = widok.OrderByDescending(x => x.data_zatr);
                    return View(widok.ToList());
                case "data_zatr":
                    widok = widok.OrderBy(x => x.data_zatr);
                    return View(widok.ToList());
                case "miasto desc":
                    widok = widok.OrderByDescending(x => x.miasto);
                    return View(widok.ToList());
                case "miasto":
                    widok = widok.OrderBy(x => x.miasto);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());
            }
        }
        #region // CreatePracownik
        public ActionResult CreatePracownik()
        {
            var wypozyczalnie = _db.wypozyczalnias.ToList();
            var list = new List<int>();
            foreach (var c in wypozyczalnie)
            {
                list.Add(c.id);
            }
            ViewBag.list = list;

            return View();
        }
        //
        [HttpPost]
        public ActionResult CreatePracownik(pracownik newPracownik)
        {

            try
            {
                _db.pracowniks.Add(newPracownik);
                _db.SaveChanges();
                return RedirectToAction("Pracownik");
            }
            catch
            {
                return View(newPracownik);
            }
        }
        #endregion

        #region // EditPracownik
        public ActionResult EditPracownik(int id)
        {
            var wypozyczalnie = _db.wypozyczalnias.ToList();
            var list = new List<int>();
            foreach (var c in wypozyczalnie)
            {
                list.Add(c.id);
            }
            ViewBag.list = list;
            var pracownikToEdit = _db.pracowniks.Find(id);
            return View(pracownikToEdit);
        }
        [HttpPost]
        public ActionResult EditPracownik(pracownik pracownikToEdit)
        {
            var orginalPracownik = _db.pracowniks.Find(pracownikToEdit.id);
            try
            {
                if (TryUpdateModel(orginalPracownik,
                    new string[] { "wypozyczalnia_id", "imie", "nazwisko", "data_zatr", "nr_telefonu", "ulica", "miasto", "kod" }))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Pracownik");
            }
            catch
            {
                return View(orginalPracownik);
            }
        }
        #endregion

        #region // DeletePracownik
        public ActionResult DeletePracownik(int id)
        {
            var pracownikToDelete = _db.pracowniks.Find(id);
            return View(pracownikToDelete);
        }
        [HttpPost]
        public ActionResult DeletePracownik(pracownik pracownikToDelete)
        {
            try
            {
                var orginalPracownik = _db.pracowniks.Find(pracownikToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalPracownik);
                }
                _db.pracowniks.Remove(orginalPracownik);
                _db.SaveChanges();
                return RedirectToAction("Pracownik");
            }
            catch
            {
                return View(pracownikToDelete);
            }
        }
        #endregion

        #region // DetailsWypozyczalnia
        public ActionResult DetailsPracownik(int id)
        {
            var pracownikToDetails = _db.pracowniks.Find(id);
            return View(pracownikToDetails);
        }
        #endregion
        #endregion

        #region // Klient
        [Authorize]
        [HttpGet]
        public ActionResult Klient(string searching, string sorting)
        {
            ViewBag.nazwisko = sorting == "nazwisko" ? "nazwisko desc" : "nazwisko";
            ViewBag.miasto = sorting == "miasto" ? "miasto desc" : "miasto";

            var widok = (_db.klients.Where(x => x.imie.StartsWith(searching) || x.kod.StartsWith(searching) || x.nazwisko.StartsWith(searching)
             || x.nip.StartsWith(searching) || x.miasto.StartsWith(searching) || x.numer_domu.StartsWith(searching) ||
             x.ulica.StartsWith(searching) || x.nr_telefonu.StartsWith(searching) || searching == null));

            switch (sorting)
            {
                case "nazwisko":
                    widok = widok.OrderBy(x => x.nazwisko);
                    return View(widok.ToList());
                case "nazwisko desc":
                    widok = widok.OrderByDescending(x => x.nazwisko);
                    return View(widok.ToList());
                case "miasto desc":
                    widok = widok.OrderByDescending(x => x.miasto);
                    return View(widok.ToList());
                case "miasto":
                    widok = widok.OrderBy(x => x.miasto);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());
            }
        }

        #region // CreateKlient
        public ActionResult CreateKlient()
        {
            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateKlient(klient newKlient)
        {
            try
            {
                _db.klients.Add(newKlient);
                _db.SaveChanges();
                return RedirectToAction("Klient");
            }
            catch
            {
                return View(newKlient);
            }
        }
        #endregion

        #region // EditKlient
        public ActionResult EditKlient(int id)
        {
            var klientToEdit = _db.klients.Find(id);
            return View(klientToEdit);
        }
        [HttpPost]
        public ActionResult EditKlient(klient klientToEdit)
        {
            var orginalKlient = _db.klients.Find(klientToEdit.id);
            try
            {
                if (TryUpdateModel(orginalKlient,
                    new string[] { "imie", "nazwisko", "ulica", "miasto", "numer_domu", "kod", "nip", "nr_telefonu" }))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Klient");
            }
            catch
            {
                return View(orginalKlient);
            }
        }
        #endregion

        #region // DeleteKlient
        public ActionResult DeleteKlient(int id)
        {
            var klientToDelete = _db.klients.Find(id);
            return View(klientToDelete);
        }
        [HttpPost]
        public ActionResult DeleteKlient(klient klientToDelete)
        {
            try
            {
                var orginalKlient = _db.klients.Find(klientToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalKlient);
                }
                _db.klients.Remove(orginalKlient);
                _db.SaveChanges();
                return RedirectToAction("Klient");
            }
            catch
            {
                return View(klientToDelete);
            }
        }
        #endregion

        #region // DetailsWypozyczalnia
        public ActionResult DetailsKlient(int id)
        {
            var klientToDetails = _db.klients.Find(id);
            return View(klientToDetails);
        }
        #endregion
        #endregion

        #region //Samochod
        [Authorize]
        [HttpGet]
        public ActionResult Samochod(string searching, string sorting)
        {
            ViewBag.marka = sorting == "marka" ? "marka desc" : "marka";
            ViewBag.model = sorting == "model" ? "model desc" : "model";
            ViewBag.poj_silnika = sorting == "poj_silnika" ? "poj_silnika desc" : "poj_silnika";
            ViewBag.cena = sorting == "cena" ? "cena desc" : "cena";
            ViewBag.rok_prod = sorting == "rok_prod" ? "rok_prod desc" : "rok_prod";
            ViewBag.segment = sorting == "segment" ? "segment desc" : "segment";

            var widok =(_db.samochods.Where(x => x.marka.StartsWith(searching) || x.model.StartsWith(searching) 
            || x.kolor.StartsWith(searching) || x.poj_silnika.ToString().StartsWith(searching) ||
            x.opis.StartsWith(searching) || x.cena.ToString().StartsWith(searching) || x.ilosc_drzwi.ToString().StartsWith(searching) 
            ||searching == null));

            switch (sorting)
            {
                case "marka":
                    widok = widok.OrderBy(x => x.marka);
                    return View(widok.ToList());
                case "marka desc":
                    widok = widok.OrderByDescending(x => x.marka);
                    return View(widok.ToList());
                case "model desc":
                    widok = widok.OrderByDescending(x => x.model);
                    return View(widok.ToList());
                case "model":
                    widok = widok.OrderBy(x => x.model);
                    return View(widok.ToList());
                case "poj_silnika desc":
                    widok = widok.OrderByDescending(x => x.poj_silnika);
                    return View(widok.ToList());
                case "poj_silnika":
                    widok = widok.OrderBy(x => x.poj_silnika);
                    return View(widok.ToList());
                case "cena desc":
                    widok = widok.OrderByDescending(x => x.cena);
                    return View(widok.ToList());
                case "cena":
                    widok = widok.OrderBy(x => x.cena);
                    return View(widok.ToList());
                case "segment desc":
                    widok = widok.OrderByDescending(x => x.kategoria.segment);
                    return View(widok.ToList());
                case "segment":
                    widok = widok.OrderBy(x => x.kategoria.segment);
                    return View(widok.ToList());
                case "rok_prod desc":
                    widok = widok.OrderByDescending(x => x.rok_prod);
                    return View(widok.ToList());
                case "rok_prod":
                    widok = widok.OrderBy(x => x.rok_prod);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());


            }
        }
        #region // CreateSamochod
        public ActionResult CreateSamochod()
        {
            var wypozyczalnie = _db.wypozyczalnias.ToList();
            var listW = new List<int>();
            foreach (var c in wypozyczalnie)
            {
                listW.Add(c.id);
            }
            ViewBag.listW = listW;

            var kategorie = _db.kategorias.ToList();
            var listK = new List<int>();
            foreach (var c in kategorie)
            {
                listK.Add(c.id);
            }
            ViewBag.listK = listK;

            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateSamochod(samochod newSamochod)
        {

            try
            {
                _db.samochods.Add(newSamochod);
                _db.SaveChanges();
                return RedirectToAction("Samochod");
            }
            catch
            {
                return View(newSamochod);
            }
        }
        #endregion

        #region // EditSamochod
        public ActionResult EditSamochod(int id)
        {
            var wypozyczalnie = _db.wypozyczalnias.ToList();
            var listW = new List<int>();
            foreach (var c in wypozyczalnie)
            {
                listW.Add(c.id);
            }
            ViewBag.listW = listW;

            var kategorie = _db.kategorias.ToList();
            var listK = new List<int>();
            foreach (var c in kategorie)
            {
                listK.Add(c.id);
            }
            ViewBag.listK = listK;

            var samochodToEdit = _db.samochods.Find(id);
            return View(samochodToEdit);
        }
        [HttpPost]
        public ActionResult EditSamochod(samochod samochodToEdit)
        {
            var orginalSamochod = _db.samochods.Find(samochodToEdit.id);
            try
            {
                if (TryUpdateModel(orginalSamochod,
                    new string[] { "wypozyczalnia_id", "kategoria",
                        "marka", "model", "rok_prod", "kolor", "poj_silnika", "opis",
                        "cena", "ilosc_drzwi"}))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Samochod");
            }
            catch
            {
                return View(orginalSamochod);
            }
        }
        #endregion

        #region // DeleteSamochod
        public ActionResult DeleteSamochod(int id)
        {
            var samochodToDelete = _db.samochods.Find(id);
            return View(samochodToDelete);
        }
        [HttpPost]
        public ActionResult DeleteSamochod(samochod samochodToDelete)
        {
            try
            {
                var orginalSamochod = _db.samochods.Find(samochodToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalSamochod);
                }
                _db.samochods.Remove(orginalSamochod);
                _db.SaveChanges();
                return RedirectToAction("Samochod");
            }
            catch
            {
                return View(samochodToDelete);
            }
        }
        #endregion

        #region // DetailsSamochod
        public ActionResult DetailsSamochod(int id)
        {
            var samochodToDetails = _db.samochods.Find(id);
            return View(samochodToDetails);
        }
        #endregion
        #endregion

        #region // Kategoria
        [Authorize]
        [HttpGet]
        public ActionResult Kategoria(string searching, string sorting)
        {
            ViewBag.segment = sorting == "segment" ? "segment desc" : "segment";

            var widok =  _db.kategorias.Where(x => x.segment.StartsWith(searching) || searching == null);

            switch(sorting)
            {
                case "segment":
                    widok = widok.OrderBy(x=>x.segment);
                    return View(widok.ToList());
                case "segment desc":
                    widok = widok.OrderByDescending(x => x.segment);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());
            }
        }

        #region // CreateKategoria
        public ActionResult CreateKategoria()
        {
            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateKategoria(kategoria newKategoria)
        {
            try
            {
                _db.kategorias.Add(newKategoria);
                _db.SaveChanges();
                return RedirectToAction("Kategoria");
            }
            catch
            {
                return View(newKategoria);
            }
        }
        #endregion

        #region // EditKategoria
        public ActionResult EditKategoria(int id)
        {
            var kategoriaToEdit = _db.kategorias.Find(id);
            return View(kategoriaToEdit);


        }
        [HttpPost]
        public ActionResult EditKategoria(kategoria kategoriaToEdit)
        {
            var orginalKategoria = _db.kategorias.Find(kategoriaToEdit.id);
            try
            {
                if (TryUpdateModel(orginalKategoria,
                    new string[] { "segment"}))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Kategoria");
            }
            catch
            {
                return View(orginalKategoria);
            }
        }
        #endregion

        #region // DeleteKategoria
        public ActionResult DeleteKategoria(int id)
        {
            var kategoriaToDelete = _db.kategorias.Find(id);
            return View(kategoriaToDelete);
        }
        [HttpPost]
        public ActionResult DeleteKategoria(kategoria kategoriaToDelete)
        {
            try
            {
                var orginalKategoria = _db.kategorias.Find(kategoriaToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalKategoria);
                }
                _db.kategorias.Remove(orginalKategoria);
                _db.SaveChanges();
                return RedirectToAction("Kategoria");
            }
            catch
            {
                return View(kategoriaToDelete);
            }
        }
        #endregion

        #region // DetailsWypozyczalnia
        public ActionResult DetailsKategoria(int id)
        {
            var kategoriaToDetails = _db.kategorias.Find(id);
            return View(kategoriaToDetails);
        }
        #endregion
        #endregion

        #region //Wypozyczenie
        [Authorize]
        [HttpGet]
        public ActionResult Wypozyczenie(string searching, string sorting)
        {
            ViewBag.nazwisko = sorting == "nazwisko" ? "nazwisko desc" : "nazwisko";
            ViewBag.nazwa = sorting == "nazwa" ? "nazwa desc" : "nazwa";

            var widok = (_db.wypozyczenie_has_pracownik.Where(x => x.pracownik.nazwisko.Contains(searching) ||
            x.pracownik.wypozyczalnia.nazwa.Contains(searching) || searching == null));

            switch (sorting)
            {
                case "nazwisko":
                    widok = widok.OrderBy(x => x.pracownik.nazwisko);
                    return View(widok.ToList());
                case "nazwisko desc":
                    widok = widok.OrderByDescending(x => x.pracownik.nazwisko);
                    return View(widok.ToList());
                case "nazwa desc":
                    widok = widok.OrderByDescending(x => x.pracownik.wypozyczalnia.nazwa);
                    return View(widok.ToList());
                case "nazwa":
                    widok = widok.OrderBy(x => x.pracownik.wypozyczalnia.nazwa);
                    return View(widok.ToList());
                default:
                    return View(widok.ToList());
            }
        }

        #region // CreateWypozyczenie
        public ActionResult CreateWypozyczenie()
        {
            var wyp = _db.wypozyczenies.ToList();
            var listW = new List<int>();
            foreach (var c in wyp)
            {
                listW.Add(c.id);
            }
            ViewBag.listW = listW;

            var prac = _db.pracowniks.ToList();
            var listP = new List<int>();
            foreach (var c in prac)
            {
                listP.Add(c.id);
            }
            ViewBag.listP = listP;

            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateWypozyczenie(wypozyczenie_has_pracownik newWypozyczenie)
        {

            try
            {
                _db.wypozyczenie_has_pracownik.Add(newWypozyczenie);
                _db.SaveChanges();
                return RedirectToAction("Wypozyczenie");
            }
            catch
            {
                return View(newWypozyczenie);
            }
        }
        #endregion

        #region // EditWypozycznie
        public ActionResult EditWypozyczenie(int wypozyczenie_id, int pracownik_id)
        {
            var wyp = _db.wypozyczenies.ToList();
            var listW = new List<int>();
            foreach (var c in wyp)
            {
                listW.Add(c.id);
            }
            ViewBag.listW = listW;

            var prac = _db.pracowniks.ToList();
            var listP = new List<int>();
            foreach (var c in prac)
            {
                listP.Add(c.id);
            }
            ViewBag.listP = listP;

            var wypozyczenieToEdit = _db.wypozyczenie_has_pracownik.Find(wypozyczenie_id,pracownik_id);
            return View(wypozyczenieToEdit);
        }
        [HttpPost]
        public ActionResult EditWypozyczenie(wypozyczenie_has_pracownik wypozyczenieToEdit)
        {
            var orginalWypozyczenie = _db.wypozyczenie_has_pracownik.Find(wypozyczenieToEdit.wypozyczenie_id, wypozyczenieToEdit.pracownik_id);
            try
            {
                if (TryUpdateModel(orginalWypozyczenie,
                    new string[] { "wypozyczalnia_id", "pracownik_id",
                        "wyp_odd"}))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Wypozyczenie");
            }
            catch
            {
                return View(orginalWypozyczenie);
            }
        }
        #endregion

        #region // DeleteWypozyczenie
        public ActionResult DeleteWypozyczenie(int wypozyczenie_id, int pracownik_id)
        {
            var wypozyczenieToDelete = _db.wypozyczenie_has_pracownik.Find(wypozyczenie_id, pracownik_id);
            return View(wypozyczenieToDelete);
        }
        [HttpPost]
        public ActionResult DeleteWypozyczenie(wypozyczenie_has_pracownik wypozyczenieToDelete)
        {
            try
            {
                var orginalWypozyczenie = _db.wypozyczenie_has_pracownik.Find(wypozyczenieToDelete.wypozyczenie_id,
                                                                              wypozyczenieToDelete.pracownik_id);
                if (!ModelState.IsValid)
                {
                    return View(orginalWypozyczenie);
                }
                _db.wypozyczenie_has_pracownik.Remove(orginalWypozyczenie);
                _db.SaveChanges();
                return RedirectToAction("Wypozyczenie");
            }
            catch
            {
                return View(wypozyczenieToDelete);
            }
        }
        #endregion

        #region // DetailsWypozyczenie
        public ActionResult DetailsWypozyczenie(int wypozyczenie_id, int pracownik_id)
        {
            var wypozyczenieToDetails = _db.wypozyczenie_has_pracownik.Find(wypozyczenie_id, pracownik_id);
            return View(wypozyczenieToDetails);
        }
        #endregion
        #endregion

        #region //Szczegoly
        [Authorize]
        [HttpGet]
        public ActionResult Szczegoly(string sorting)
        {
            ViewBag.data_wypozyczenia = sorting == "data_wypozyczenia" ? "data_wypozyczenia desc" : "data_wypozyczenia";
            ViewBag.data_konca_wypoz = sorting == "data_konca_wypoz" ? "data_konca_wypoz desc" : "data_konca_wypoz";
            ViewBag.data_oddania = sorting == "data_oddania" ? "data_oddania" : "data_oddania";
            ViewBag.cena = sorting == "cena" ? "cena desc" : "cena";
            ViewBag.nazwisko = sorting == "nazwisko" ? "nazwisko" : "nazwisko";
            ViewBag.marka = sorting == "marka" ? "marka desc" : "marka";

            var widok = (_db.wypozyczenies);

            switch (sorting)
            {
                case "data_wypozyczenia":
                    return View(widok.OrderBy(x => x.data_wypozyczenia).ToList());
                case "data_wypozyczenia desc":
                    return View(widok.OrderByDescending(x => x.data_wypozyczenia).ToList());

                case "data_konca_wypoz":
                    return View(widok.OrderBy(x => x.data_konca_wypoz).ToList());
                case "data_konca_wypoz desc":
                    return View(widok.OrderByDescending(x => x.data_konca_wypoz).ToList());

                case "data_oddania":
                    return View(widok.OrderBy(x => x.data_oddania).ToList());
                case "data_oddania desc":
                    return View(widok.OrderByDescending(x => x.data_oddania).ToList());

                case "cena":
                    return View(widok.OrderBy(x => x.cena).ToList());
                case "cena desc":
                    return View(widok.OrderByDescending(x => x.cena).ToList());

                case "nazwisko":
                    return View(widok.OrderBy(x => x.klient.nazwisko).ToList());
                case "nazwisko desc":
                    return View(widok.OrderByDescending(x => x.klient.nazwisko).ToList());

                case "marka":
                    return View(widok.OrderBy(x => x.samochod.marka).ToList());
                case "marka desc":
                    return View(widok.OrderByDescending(x => x.samochod.marka).ToList());




                default:
                    return View(widok.ToList());


            }
        }
        #region // CreateSamochod
        public ActionResult CreateSzczegoly()
        {
            var samochody= _db.samochods.ToList();
            var listS = new List<int>();
            foreach (var c in samochody)
            {
                listS.Add(c.id);
            }
            ViewBag.listS = listS;

            var klients = _db.klients.ToList();
            var listK = new List<int>();
            foreach (var c in klients)
            {
                listK.Add(c.id);
            }
            ViewBag.listK = listK;

            return View();
        }
        //
        [HttpPost]
        public ActionResult CreateSzczegoly(wypozyczenie newSzczegoly)
        {

            try
            {
                _db.wypozyczenies.Add(newSzczegoly);
                _db.SaveChanges();
                return RedirectToAction("Szczegoly");
            }
            catch
            {
                return View(newSzczegoly);
            }
        }
        #endregion

        #region // EditSzczegoly
        public ActionResult EditSzczegoly(int id)
        {
            var samochody = _db.samochods.ToList();
            var listS = new List<int>();
            foreach (var c in samochody)
            {
                listS.Add(c.id);
            }
            ViewBag.listS = listS;

            var klients = _db.klients.ToList();
            var listK = new List<int>();
            foreach (var c in klients)
            {
                listK.Add(c.id);
            }
            ViewBag.listK = listK;

            var szczegolyToEdit = _db.wypozyczenies.Find(id);
            return View(szczegolyToEdit);
        }
        [HttpPost]
        public ActionResult EditSzczegoly(wypozyczenie szczegolyToEdit)
        {
            var orginalSzczegoly = _db.wypozyczenies.Find(szczegolyToEdit.id);
            try
            {
                if (TryUpdateModel(orginalSzczegoly,
                    new string[] { "samochody_id", "klient_id",
                        "data_wypozyczenia", "data_konca_wypoz", "data_oddania", "cena"}))
                {
                    _db.SaveChanges();
                }
                return RedirectToAction("Szczegoly");
            }
            catch
            {
                return View(orginalSzczegoly);
            }
        }
        #endregion

        #region // DeleteSamochod
        public ActionResult DeleteSzczegoly(int id)
        {
            var szczegolyToDelete = _db.wypozyczenies.Find(id);
            return View(szczegolyToDelete);
        }
        [HttpPost]
        public ActionResult DeleteSzczegoly(wypozyczenie szczegolyToDelete)
        {
            try
            {
                var orginalSzczegoly = _db.wypozyczenies.Find(szczegolyToDelete.id);
                if (!ModelState.IsValid)
                {
                    return View(orginalSzczegoly);
                }
                _db.wypozyczenies.Remove(orginalSzczegoly);
                _db.SaveChanges();
                return RedirectToAction("Szczegoly");
            }
            catch
            {
                return View(szczegolyToDelete);
            }
        }
        #endregion

        #region // DetailsSzczegoly
        public ActionResult DetailsSzczegoly(int id)
        {
            var szczegolyToDetails = _db.wypozyczenies.Find(id);
            return View(szczegolyToDetails);
        }
        #endregion
        #endregion

        #region //Zdjecia
        [HttpGet]
        [Authorize]
        public ActionResult Zdjecia()
        {
            return View();
        }
        #endregion

    }

}
