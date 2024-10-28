# Probleme de rezolvat
## 1. Care este ordinea de desenare a vertexurilor pentru aceste metode (orar sau anti-orar)?
In mod implicit, OpenGL foloseste o ordine anti-orara pentru desenarea fetelor vizibile. Aceasta determina ce fata este vizibila si care este ascunsa cand este activat culling-ul(eliminarea automata a fetelor sau poligoanelor care nu sunt vizibile in scena 3D).

## 2. Ce este anti-aliasing? Prezentati aceasta tehnica pe scurt.
Anti-aliasing este o tehnica utilizata pentru a netezi marginile aspre ale graficii digitale, reducand efectul de "dinti de fierastrau" de la marginea obiectelor. Acesta implica interpolarea culorilor de-a lungul marginilor obiectului pentru a crea o tranzitie mai lina intre margini si fundal.

## 3. Care este efectul rularii comenzii GL.LineWidth(float)? Dar pentru GL.PointSize(float)? Functioneaza in interiorul unei zone GL.Begin()?
GL.LineWidth(float) seteaza latimea liniilor desenate.
GL.PointSize(float) seteaza dimensiunea punctelor.
Ambele functioneaza in interiorul secventei GL.Begin() si au efect asupra tuturor liniilor si punctelor desenate ulterior.

## 4. Raspundeti la urmatoarele intrebari:
### Care este efectul utilizarii directivei LineLoop atunci cand desenate segmente de dreapta multiple in OpenGL?
Conecteaza ultima linie la prima pentru a inchide forma.

### Care este efectul utilizarii directivei LineStrip atunci cand desenate segmente de dreapta multiple in OpenGL?
Deseneaza o linie continua prin conectarea fiecarui vertex.

### Care este efectul utilizarii directivei TriangleFan atunci cand desenate segmente de dreapta multiple in OpenGL?
Creeaza un triunghi pentru fiecare segment, pornind din primul vertex (folosit pentru forme precum cercuri).

### Care este efectul utilizarii directivei TriangleStrip atunci cand desenate segmente de dreapta multiple in OpenGL?
Creeaza triunghiuri consecutive, alternand ordinea pentru fiecare segment, ideal pentru suprafete continue.

## 6. De ce este importanta utilizarea de culori diferite (in gradient sau culori selectate per suprafata) in desenarea obiectelor 3D? Care este avantajul?
Culorile diferite ajuta la diferentierea fatetelor si ofera un efect de profunzime, permitand interpretarea mai usoara a formei obiectelor 3D.

## 7. Ce reprezinta un gradient de culoare? Cum se obtine acesta in OpenGL?
Gradientul este tranzitia graduala intre doua sau mai multe culori. in OpenGL, se obtine specificand culori diferite pentru fiecare vertex.

## 10. Ce efect are utilizarea unei culori diferite pentru fiecare vertex atunci cand desenati o linie sau un triunghi in modul strip?
Folosirea culorilor diferite pentru fiecare vertex creeaza un gradient de-a lungul liniei sau triunghiului, realizand o tranzitie lina intre culorile specificate pentru fiecare vertex.