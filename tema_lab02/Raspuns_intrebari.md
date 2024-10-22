# Raspunsuri intrebari

## 1. Ce este un viewport?
Un 'viewport' reprezinta zona din fereastra de afisare unde se va desena continutul grafic. Acesta defineste dimensiunea si pozitia regiunii de afisare pe care OpenGL va reda scenele 3D. Poate fi setat folosind functia 'glViewport', care primeste ca parametri coordonatele x si y ale coltului stanga-jos al viewport-ului si latimea si inaltimea acestuia.

## 2. Ce reprezinta conceptul de frames per second (FPS) din punctul de vedere al bibliotecii OpenGL?
'Frames per second (FPS)' reprezinta numarul de cadre (sau imagini) care sunt redat in fiecare secunda. Este o masura a performantei aplicatiilor grafice. In OpenGL, un FPS mai mare indica o experienta de vizionare mai fluida, in timp ce un FPS scazut poate duce la intreruperi vizibile sau la o performanta slaba.

## 3. Cand este rulata metoda OnUpdateFrame()?
Metoda 'OnUpdateFrame()' este rulata la fiecare cadru (frame) si este folosita pentru a actualiza starea aplicatiei, cum ar fi gestionarea intrarilor de la utilizator, animarea obiectelor sau actualizarea logicii jocului. Aceasta este un loc ideal pentru a aplica transformari sau pentru a actualiza datele care vor fi apoi utilizate in metoda de redare.

## 4. Ce este modul imediat de randare?
'Modul imediat de randare' (Immediate Mode) este un stil de randare in care comenzile de desenare sunt trimise direct la GPU fara a utiliza un buffer intermediar. Fiecare apel la functiile de randare, cum ar fi 'glBegin()' si 'glEnd()', genereaza un cadru de grafica. Acest mod este simplu, dar este ineficient pentru aplicatii complexe, din cauza costurilor mari de apeluri de functii si gestionarea datelor.

## 5. Care este ultima versiune de OpenGL care accepta modul imediat?
'OpenGL 3.3' este considerata ultima versiune care accepta modul imediat de randare. Incepand cu OpenGL 3.0, s-a pus accent pe folosirea de tehnici de randare mai avansate, cum ar fi utilizarea bufferelor de vertex si shader-elor, iar modul imediat a fost depreciat in favoarea acestor tehnici mai eficiente.

## 6. Cand este rulata metoda OnRenderFrame()?
Metoda 'OnRenderFrame()' este rulata dupa actualizarea starii aplicatiei, in fiecare cadru. Aceasta se ocupa de toate operatiile de randare efective, cum ar fi desenarea obiectelor 3D pe ecran. Este locul unde se defineste cum ar trebui sa arate scena, utilizand datele actualizate din 'OnUpdateFrame()'.

## 7. De ce este nevoie ca metoda OnResize() sa fie executata cel putin o data?
Metoda 'OnResize()' trebuie sa fie executata cel putin o data pentru a configura corect viewport-ul si pentru a actualiza orice parametrii legati de dimensiunea ferestrei, cum ar fi matricea de proiectie. Daca aceasta metoda nu este apelata, aplicatia poate avea un viewport gresit configurat, ceea ce va afecta modul in care continutul grafic este redat.

## 8. Ce reprezinta parametrii metodei CreatePerspectiveFieldOfView() si care este domeniul de valori pentru acestia?
Metoda 'CreatePerspectiveFieldOfView()' defineste o matrice de proiectie pentru o camera cu vedere pe un unghi de camp (field of view). Parametrii acestei metode sunt:
- 'fov': unghiul de vedere (in radiani) pe verticala, care controleaza cat de larga este perspectiva. De obicei, se foloseste un interval de valori intre 0 si π (180 de grade).
- 'aspectRatio': raportul intre latimea si inaltimea ferestrei de vizualizare, care asigura ca imaginea nu este distorsionata.
- 'nearPlane': distanta de la camera pana la planul de taiere apropiat, care determina ce obiecte sunt vizibile. Valorile trebuie sa fie pozitive si mai mari decat zero.
- 'farPlane': distanta de la camera pana la planul de taiere indepartat, care defineste limita vizibilitatii pentru obiecte. De asemenea, trebuie sa fie mai mare decat nearPlane si pozitiv.