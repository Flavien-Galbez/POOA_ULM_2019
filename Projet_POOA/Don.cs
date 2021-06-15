using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;

namespace Projet_POOA
{
    //Utilisation d'une interface
    class Don : IComparable
    {
        int id;
        DateTime date_reception;
        string type_materiel;
        int ref_objet;
        Adherent donneur;
        string description_comp;

        public Don(int id, DateTime date_reception, string type_materiel, int ref_objet, Adherent donneur, string description_comp)
        {
            this.id = id;
            this.date_reception = date_reception;
            this.type_materiel = type_materiel;
            this.ref_objet = ref_objet;
            this.donneur = donneur;
            this.description_comp = description_comp;
        }

        public Don()
        { }

        public int ID
        {
            get { return id; }
        }

        public DateTime Date_reception
        {
            get { return date_reception; }
        }

        public string Type_materiel
        {
            get { return type_materiel; }
        }

        public int Ref_objet
        {
            get { return ref_objet; }
        }

        public Adherent Donneur
        {
            get { return donneur; }
        }

        public string Description_supp
        {
            get { return description_comp; }
        }

        public int CompareTo(object obj)
        {
            Don valeur = (Don)obj;
            return this.id.CompareTo(valeur.id);
        }

        /// <summary>
        /// Permets à l'utilisateur de saisir un don est de l'ajouter à la liste des dons
        /// </summary>
        /// <param name="adherents">Liste des adhérents existant</param>
        /// <param name="id_don">Id du futur don crée</param>
        /// <returns>Nouveau don crée</returns>
        public static Don Creation_don(List<Adherent> adherents, int id_don)
        {
            int ID_adh = -1;
            bool valide = false;

            Adherent donneur = new Adherent();
            do
            {
                Console.WriteLine("Quel est votre ID d'adhérent ?");
                try
                {
                    ID_adh = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    ID_adh = -1;
                }
                foreach (Adherent a in adherents)
                {
                    if (a.ID == ID_adh)
                    {
                        donneur = a;
                        valide = true;
                    }
                }
                if (!valide)
                {
                    Console.WriteLine("L'Id saisie n'est pas valide, veuillez le ressaisir");
                }
            }
            while (!valide);

            valide = false;
            DateTime date_retrait = new DateTime();
            Console.WriteLine("A quelle date allez-vous faire votre don (format jour/mois/annee): ");
            while (!valide)
            {
                try
                {
                    string date = Console.ReadLine();
                    string[] date_split = date.Split('/');
                    date_retrait = new DateTime(Convert.ToInt32(date_split[2]), Convert.ToInt32(date_split[1]), Convert.ToInt32(date_split[0]));
                    valide = true;
                }
                catch
                {
                    Console.WriteLine("La date saisie n'est pas valide, veuillez la ressaisir");
                }
            }

            Console.WriteLine("Quel est le type de matériel ?");
            string type_m = Console.ReadLine();

            Console.WriteLine("Quelle est la référence de l'objet ?");
            int ref_objet = -1;
            while (id_don < 0)
            {
                try
                {
                    ref_objet = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    id_don = -1;
                }
            }
            Console.WriteLine("Description supplémentaire (état, couleur ...)?");
            string desc = Console.ReadLine();

            Don don = new Don(id_don, date_retrait, type_m, ref_objet, donneur, desc);
            return don;
        }

        /// <summary>
        /// Permet de stocker un don
        /// </summary>
        /// <param name="don">Don à stocker</param>
        /// <param name="personnes_morales">Liste des personnes morales</param>
        /// <param name="dons_depot_vente">Liste des don en depot vente</param>
        /// <param name="dons_garde_meuble">Liste des dons en garde-meuble</param>
        /// <param name="dons_asso">Liste des dons en association</param>
        /// <param name="archives">Liste des dons en archive</param>
        /// <param name="benificiaires">Liste des bénéficiares</param>
        /// <param name="adherents">Liste des adherents</param>
        /// <param name="objets">Liste des objets</param>
        public static void Stockage_Don(Don don, List<Personne_morale> personnes_morales, List<Don_depot_vente> dons_depot_vente, List<Don_garde_meuble> dons_garde_meuble, List<Don_asso> dons_asso, ArrayList archives, List<Beneficiaire> benificiaires, List<Adherent> adherents, List<Objet> objets)
        {
            int choix = 0;
            Console.WriteLine("A quelle date allez-vous retirer le don (format jour/mois/annee): ");
            string date = Console.ReadLine();
            string[] date_split = date.Split('/');
            DateTime date_retrait = new DateTime(Convert.ToInt32(date_split[2]), Convert.ToInt32(date_split[1]), Convert.ToInt32(date_split[0]));
            Console.WriteLine("Ou voulez vous stocker votre Don :  1-Depot vente | 2-Garde meuble | 3-Asso  | 4-Supprimer le Don");
            choix = -1;
            while (choix < 1 || choix > 4)
            {
                try
                {
                    choix = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    choix = -1;
                }
            }

            if (choix == 1)
            {
                bool personne_valide = false;
                Personne_morale pm = new Personne_morale();
                while (!personne_valide)
                {
                    Console.Write("Saississez l'ID du dépôt-vente: ");
                    int id_dv = -1;
                    while (id_dv < 0)
                    {
                        try
                        {
                            id_dv = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            id_dv = -1;
                        }
                    }
                    foreach (Personne_morale p in personnes_morales)
                    {
                        if (id_dv == p.ID)
                        {
                            pm = p;
                            personne_valide = true;
                        }
                    }
                    if (!personne_valide)
                    {
                        Console.WriteLine("L'id saisie ne correspond à aucune personne, veuillez en resaisir un autre");
                    }
                }
                Console.Write("Saisir le montant du don: ");
                double montant = -1;
                while (montant < 0)
                {
                    try
                    {
                        montant = Convert.ToDouble(Console.ReadLine());
                    }
                    catch
                    {
                        montant = -1;
                    }
                }
                Don_depot_vente Don_dpv = new Don_depot_vente(date_retrait, montant, pm, don.ID, don.Date_reception, don.Type_materiel, don.Ref_objet, don.Donneur, don.Description_supp);
                Type_activite type_Activite = (Type_activite)2;
                Objet.Saisie_stockage(objets, don.Ref_objet, type_Activite);
                dons_depot_vente.Add(Don_dpv);
                Console.WriteLine("Votre don a bien été placé dans le depot vente");
            }
            if (choix == 2)
            {
                bool garde_meuble_valide = false;
                Personne_morale pm = new Personne_morale();
                while (!garde_meuble_valide)
                {
                    Console.Write("Saississez l'ID du garde-meuble: ");
                    int id_gm = -1;
                    while (id_gm < 0)
                    {
                        try
                        {
                            id_gm = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            id_gm = -1;
                        }
                    }
                    foreach (Personne_morale p in personnes_morales)
                    {
                        if (id_gm == p.ID)
                        {
                            pm = p;
                            garde_meuble_valide = true;
                        }
                    }
                    if (!garde_meuble_valide)
                    {
                        Console.WriteLine("L'id saisie ne correspond à aucun garde-meuble, veuillez en resaisir un autre");
                    }
                }
                bool beneficiare_valide = false;
                Beneficiaire b_stock = new Beneficiaire();
                while (!beneficiare_valide)
                {
                    Console.WriteLine("Quel est l'ID du bénéficiaire ?");
                    int id = -1;
                    while (id < 0)
                    {
                        try
                        {
                            id = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            id = -1;
                        }
                    }
                    foreach (Beneficiaire b2 in benificiaires)
                    {
                        if (id == b2.ID)
                        {
                            b_stock = b2;
                            beneficiare_valide = true;
                        }
                    }
                    if (!beneficiare_valide)
                    {
                        Console.WriteLine("L'id saisie ne correspond à aucun beneficiaire, veuillez en resaisir un autre");
                    }
                }
                Don_garde_meuble Don_gm = new Don_garde_meuble(date_retrait, b_stock, pm, don.ID, don.Date_reception, don.Type_materiel, don.Ref_objet, don.Donneur, don.Description_supp);
                Type_activite type_Activite = (Type_activite)1;
                Objet.Saisie_stockage(objets, don.Ref_objet, type_Activite);
                dons_garde_meuble.Add(Don_gm);
                Console.WriteLine("Votre don a bien été placé dans le garde meuble");

            }
            if (choix == 3)
            {
                Don_asso don_a = new Don_asso(date_retrait, don.ID, don.Date_reception, don.Type_materiel, don.Ref_objet, don.Donneur, don.Description_supp);
                Type_activite type_Activite = (Type_activite)0;
                Objet.Saisie_stockage(objets, don.Ref_objet, type_Activite);
                Console.WriteLine("Votre don a bien été placé dans l'association");
            }

            if (choix == 4)
            {
                archives.Add(don);
                Console.WriteLine("Le don a été archivé");
            }


        }

        /// <summary>
        /// Permet de transférer un don
        /// </summary>
        /// <param name="dons_depot_vente">Liste des don en dépot vente</param>
        /// <param name="dons_garde_meuble">Liste des dons en garde-meuble</param>
        /// <param name="benificiaires">Liste des bénéficiaires</param>
        /// <param name="dons_asso">Liste des dons en association</param>
        /// <param name="archives">Liste des dons en archive</param>
        /// <param name="objets">Liste des objets</param>
        public static void Transferer_Don(List<Don_depot_vente> dons_depot_vente, List<Don_garde_meuble> dons_garde_meuble, List<Beneficiaire> benificiaires, List<Don_asso> dons_asso, ArrayList archives, List<Objet> objets)
        {


            int id=-1;
            Console.WriteLine("Quel est l'ID du DON que vous voulez archiver ?");
            while (id <= 0)
            {
                try
                {
                    id = Convert.ToInt32(Console.ReadLine());
                }
                catch
                {
                    id = -1;
                }
            }
            Don_depot_vente don_dpv;
            int index = 0;
            bool test_dpv = false;
            Don_asso don_as;
            bool test_asso = false;
            Don_garde_meuble don_gm;
            bool test_gm = false;

            foreach (Don_depot_vente don_dpv_parcours in dons_depot_vente)
            {

                if (don_dpv_parcours.ID == id)
                {
                    don_dpv = don_dpv_parcours;
                    test_dpv = true;
                    archives.Add(don_dpv_parcours);
                    break;
                }
                index++;
            }
            if (test_dpv == true)
            {
                bool beneficiare_valide = false;
                Beneficiaire b_stock = new Beneficiaire();
                while (!beneficiare_valide)
                {
                    Console.WriteLine("Quel est l'ID du bénéficiaire ?");
                    int id_benef = -1;
                    while (id_benef < 0)
                    {
                        try
                        {
                            id_benef = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            id_benef = -1;
                        }
                    }
                    foreach (Beneficiaire b2 in benificiaires)
                    {
                        if (id_benef == b2.ID)
                        {
                            b_stock = b2;
                            beneficiare_valide = true;
                        }
                    }
                    if (!beneficiare_valide)
                    {
                        Console.WriteLine("L'id saisie ne correspond à aucun beneficiaire, veuillez en resaisir un autre");
                    }
                }
                Objet.Saisie_beneficiaire(objets, dons_depot_vente[index].Ref_objet, b_stock);
                dons_depot_vente.RemoveAt(index);
                Console.WriteLine("Le don dans le depot vente a bien été archivé");
            }
            index = 0;
            foreach (Don_asso don_ass_parours in dons_asso)
            {
                if (don_ass_parours.ID == id)
                {
                    don_as = don_ass_parours;
                    test_asso = true;
                    archives.Add(don_as);
                    break;
                }
                index++;
            }
            if (test_asso == true)
            {
                bool beneficiare_valide = false;
                Beneficiaire b_stock = new Beneficiaire();
                while (!beneficiare_valide)
                {
                    Console.WriteLine("Quel est l'ID du bénéficiaire ?");
                    int id_benef = -1;
                    while (id_benef < 0)
                    {
                        try
                        {
                            id_benef = Convert.ToInt32(Console.ReadLine());
                        }
                        catch
                        {
                            id_benef = -1;
                        }
                    }
                    foreach (Beneficiaire b2 in benificiaires)
                    {
                        if (id_benef == b2.ID)
                        {
                            b_stock = b2;
                            beneficiare_valide = true;
                        }
                    }
                    if (!beneficiare_valide)
                    {
                        Console.WriteLine("L'id saisie ne correspond à aucun beneficiaire, veuillez en resaisir un autre");
                    }
                }
                Objet.Saisie_beneficiaire(objets, dons_asso[index].Ref_objet, b_stock);
                dons_depot_vente.RemoveAt(index);
                dons_asso.RemoveAt(index);
                Console.WriteLine("Le don dans l'asso a bien été archivé");
            }

            index = 0;
            foreach (Don_garde_meuble don_gm_parcours in dons_garde_meuble)
            {
                if (don_gm_parcours.ID == id)
                {
                    don_gm = don_gm_parcours;
                    test_gm = true;
                    archives.Add(don_gm);
                    break;
                }
                index++;
            }
            if (test_gm == true)
            {
                Objet.Saisie_beneficiaire(objets, dons_garde_meuble[index].Ref_objet, dons_garde_meuble[index].Benef);
                dons_garde_meuble.RemoveAt(index);
                Console.WriteLine("Le don dans le garde meuble a bien été archivé");
            }

            if (!test_gm && !test_asso && !test_dpv)
            {
                Console.WriteLine("Le don n'a pas été trouvé dans une des listes");
            }

        }

        /// <summary>
        /// Permet d'archiver un don
        /// </summary>
        /// <param name="dons_depot_vente">Liste des don en dépot vente</param>
        /// <param name="dons_garde_meuble">Liste des dons en garde-meuble</param>
        /// <param name="dons_asso">Liste des dons en association</param>
        /// <param name="archives">Liste des dons en archive</param>
        public static void Archive_Don(List<Don_depot_vente> dons_depot_vente, List<Don_garde_meuble> dons_garde_meuble, List<Don_asso> dons_asso, ArrayList archives)
        {

            int id;
            Console.WriteLine("Quel est l'ID du DON que vous voulez archiver ?");
            try
            {
                id = Convert.ToInt32(Console.ReadLine());
            }
            catch
            {
                id = -1;
            }
            Don_depot_vente don_dpv;
            int index = 0;
            bool test_dpv = false;
            Don_asso don_as;
            bool test_asso = false;
            Don_garde_meuble don_gm;
            bool test_gm = false;

            foreach (Don_depot_vente don_dpv_parcours in dons_depot_vente)
            {

                if (don_dpv_parcours.ID == id)
                {
                    don_dpv = don_dpv_parcours;
                    test_dpv = true;
                    archives.Add(don_dpv_parcours);
                    break;
                }
                index++;
            }
            if (test_dpv == true)
            {
                dons_depot_vente.RemoveAt(index);
                Console.WriteLine("Le don dans le depot vente a bien été archivé");
            }
            index = 0;
            foreach (Don_asso don_ass_parours in dons_asso)
            {
                if (don_ass_parours.ID == id)
                {
                    don_as = don_ass_parours;
                    test_asso = true;
                    archives.Add(don_as);
                    break;
                }
                index++;
            }
            if (test_asso == true)
            {
                dons_asso.RemoveAt(index);
                Console.WriteLine("Le don dans l'asso a bien été archivé");
            }

            index = 0;
            foreach (Don_garde_meuble don_gm_parcours in dons_garde_meuble)
            {
                if (don_gm_parcours.ID == id)
                {
                    don_gm = don_gm_parcours;
                    test_gm = true;
                    archives.Add(don_gm);
                    break;
                }
                index++;
            }
            if (test_gm == true)
            {
                dons_garde_meuble.RemoveAt(index);
                Console.WriteLine("Le don dans le garde meuble a bien été archivé");
            }

            if (!test_gm && !test_asso && !test_dpv)
            {
                Console.WriteLine("Le don n'a pas été trouvé dans une des listes");
            }

        }

        /// <summary>
        /// Permet d'afficher un don
        /// </summary>
        /// <param name="dons">Liste des dons</param>
        public static void Afficher_Dons(List<Don> dons)
        {
            Console.WriteLine("Les dons à trier sont:");
            foreach (Don d in dons)
            {
                Console.Write(d.ID);
                Console.WriteLine("   " + d.Type_materiel + "\n");
            }
        }

        /// <summary>
        /// Permet de trier les dons refusés
        /// </summary>
        /// <param name="archives">Liste des dons archivés</param>
        public static void Tri_don_refuse(ArrayList archives)
        {
            List<Don> don_refuses = new List<Don>();
            foreach (object o in archives)
            {
                if (o.GetType() == typeof(Don))
                {
                    don_refuses.Add((Don)o);
                }
            }

            don_refuses.Sort((x, y) => x.Date_reception.CompareTo(y.Date_reception));
            Console.WriteLine("Les dons triés dans l'ordre sont: ");
            foreach (Don d in don_refuses)
            {
                Console.WriteLine(d.ID + "   " + d.Date_reception);
            }
        }

        /// <summary>
        /// Permet de trier les les dons en traitement (accepté ou stocké) par ordre d’identifiant et de nom de donataire
        /// </summary>
        /// <param name="dons_depot_vente">Liste don en dépot vente</param>
        /// <param name="dons_garde_meuble">Liste des don en garde-meuble</param>
        /// <param name="dons_asso">Liste des don en association</param>
        public static void Tri_traitement_stock(List<Don_depot_vente> dons_depot_vente, List<Don_garde_meuble> dons_garde_meuble, List<Don_asso> dons_asso)
        {
            List<Don> liste_tri = new List<Don>();
            foreach (Don_depot_vente dpv in dons_depot_vente)
            {
                Don don = new Don(dpv.ID, dpv.Date_reception, dpv.Type_materiel, dpv.Ref_objet, dpv.Donneur, dpv.Description_supp);
                liste_tri.Add(don);
            }
            foreach (Don_garde_meuble dgm in dons_garde_meuble)
            {
                Don don = new Don(dgm.ID, dgm.Date_reception, dgm.Type_materiel, dgm.Ref_objet, dgm.Donneur, dgm.Description_supp);
                liste_tri.Add(don);
            }
            foreach (Don_asso da in dons_asso)
            {
                Don don = new Don(da.ID, da.Date_reception, da.Type_materiel, da.Ref_objet, da.Donneur,da.Description_supp);
                liste_tri.Add(don);
            }

            liste_tri.Sort((x, y) => x.ID.CompareTo(y.ID));
            liste_tri.Sort((x, y) => x.Donneur.Nom.CompareTo(y.Donneur.Nom));
            Console.WriteLine("Les dons en traitement ou stockés triés par ID et nom sont: ");
            foreach (Don d in liste_tri)
            {
                Console.WriteLine(d.ID + "   " + d.Type_materiel + "   " + d.Donneur.Nom);
            }
        }

    }

    class Don_depot_vente : Don, IComparable
    {
        DateTime date_vente;
        Personne_morale depot;
        double montant;

        public Don_depot_vente(DateTime date_vente, double montant, Personne_morale depotv, int id, DateTime date_reception, string type_materiel, int ref_objet, Adherent donneur, string description_comp)
            : base(id, date_reception, type_materiel, ref_objet, donneur, description_comp)
        {
            this.date_vente = date_vente;
            this.montant = montant;
            this.depot = depotv;
        }
        public Don_depot_vente()
        { }

        public DateTime Date_vente
        {
            get { return date_vente; }
        }

        public Personne_morale Depot
        {
            get { return depot; }
        }

        public double Montant
        {
            get { return montant; }
        }

        /// <summary>
        /// Tri les dons par catégorie
        /// </summary>
        /// <param name="dons_depot_vente">Liste des dons en depot vente</param>
        public static void Tri_don_par_entrepot_categorie(List<Don_depot_vente> dons_depot_vente)
        {
            List<Don_depot_vente> don = new List<Don_depot_vente>();
            foreach (Don_depot_vente dv in dons_depot_vente)
            {
                Don_depot_vente Don2 = new Don_depot_vente(dv.Date_vente, dv.Montant, dv.Depot, dv.ID, dv.Date_reception, dv.Type_materiel, dv.Ref_objet, dv.Donneur, dv.Description_supp);
                don.Add(Don2);
            }
            don.Sort((x, y) => x.Depot.ID.CompareTo(y.Depot.ID));
            don.Sort((x, y) => x.Type_materiel.CompareTo(y.Type_materiel));
            Console.WriteLine("Les dons stockés triés par entrepot et type sont: ");

            foreach (Don_depot_vente d in don)
            {
                Console.WriteLine("ID du don :" + d.ID + "         " + d.Type_materiel + "     ID : Entrepot:" + d.Depot.ID);
            }
        }

        /// <summary>
        /// Tri les dons par prix
        /// </summary>
        /// <param name="dons_depot_vente">Liste des dons en depot vente</param>
        public static void Tri_par_depot_vente_prix(List<Don_depot_vente> dons_depot_vente)
        {
            List<Don_depot_vente> dons = new List<Don_depot_vente>();
            foreach (Don_depot_vente dv in dons_depot_vente)
            {
                Don_depot_vente Don2 = new Don_depot_vente(dv.Date_vente, dv.Montant, dv.Depot, dv.ID, dv.Date_reception, dv.Type_materiel, dv.Ref_objet, dv.Donneur, dv.Description_supp);
                dons.Add(Don2);
            }
            dons.Sort((x, y) => x.Depot.ID.CompareTo(y.Depot.ID));
            dons.Sort((x, y) => x.Montant.CompareTo(y.Montant));
            Console.WriteLine("Les dons triés par depôt vente et par prix: ");

            foreach (Don_depot_vente d in dons)
            {
                Console.WriteLine("ID du don :" + d.ID + "         " + d.Type_materiel + "     ID : Entrepot:" + d.Depot.ID + "   , prix " + d.Montant);
            }
        }

    }

    class Don_garde_meuble : Don, IComparable
    {
        DateTime date_enlevement;
        Beneficiaire benef;
        Personne_morale garde_meuble;

        public Don_garde_meuble(DateTime date_enlevement, Beneficiaire benef, Personne_morale garde_meuble, int id, DateTime date_reception, string type_materiel, int ref_objet, Adherent donneur, string description_comp)
             : base(id, date_reception, type_materiel, ref_objet, donneur, description_comp)
        {
            this.date_enlevement = date_enlevement;
            this.benef = benef;
            this.garde_meuble = garde_meuble;
        }

        public Don_garde_meuble()
        { }


        public DateTime Date_enlevement
        {
            get { return date_enlevement; }
        }

        public Personne_morale Garde_meuble
        {
            get { return garde_meuble; }
        }

        public Beneficiaire Benef
        {
            get { return benef; }
        }

        /// <summary>
        /// Tri lesvendus/donnés par mois/numéro de bénéficiare
        /// </summary>
        /// <param name="liste_gm">Liste des dons garde-meuble</param>
        public static void Tri_don_vendu_donnes(List<Don_garde_meuble> liste_gm)
        {
            List<Don_garde_meuble> liste_tri = new List<Don_garde_meuble>();

            foreach (Don_garde_meuble dgm in liste_gm)
            {
                liste_tri.Add(dgm);
            }


            liste_tri.Sort((x, y) => x.Date_reception.Month.CompareTo(y.Date_reception.Month));
            liste_tri.Sort((x, y) => x.Benef.ID.CompareTo(y.Benef.ID));
            Console.WriteLine("Les dons vendus/donnés triés par mois et numéro de bénéficiaire sont: ");
            foreach (Don_garde_meuble d in liste_tri)
            {
                Console.WriteLine(d.ID + " " +
                    "  " + "      " + d.Date_reception.Month + "         " + d.Type_materiel + "     ID : Beneficiaire:" + d.Benef.ID);
            }
        }
    }

    class Don_asso : Don, IComparable
    {
        DateTime date_retrait;

        public Don_asso(DateTime date_retrait, int id, DateTime date_reception, string type_materiel, int ref_objet, Adherent donneur, string description_comp)
             : base(id, date_reception, type_materiel, ref_objet, donneur, description_comp)
        {
            this.date_retrait = date_retrait;
        }

        public Don_asso()
        { }

        public DateTime Date_retrait
        {
            get { return date_retrait; }
        }
    }

}
