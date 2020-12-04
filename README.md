# Modellbahn
## remind me to do it in C#

this is a program to controll the modellbahn

i am currently re-writing it in C# or C++ i don't really know at the moment 

## function
### Gleisplan

you as a user can add different object to build your own "Gleisplan"
simply click on the object you want to build in the bar at the top or use the hotkeys

G -> gleis

W -> weiche

B -> besetztmelder

L -> delete (loeschen)

#### Gleis

for Gleise you need to click on the screen two times so the two points can be connected to a line

#### Weiche

for Weichen you need to click at the location where you want to place the Weiche and move your cursor untill the orientation of the weiche fits your needs
then you will have to specify the decoder and byte values of the Weiche

#### Kreuzweichen

Kreuzweichen are a special type of Weichen
if you want to build one you will need to select the Kreuzweichenbutton at the drop-down-box in the bar at the top

after clicking on the position where you want to place the Kreuzweiche you will be asked what type of Kreuzweiche it should be
simply choose your typr in the dialoge at the middle of the screen that will pop-up

#### Besetztmelder

for Besetztmelder simply mark the Gleis you want to choose and specify the decoder and byte values of the Besetztmelder

#### Deleting

if you want to delete an object click the delete-button and hover of the object to delete it

#### Save a Gleisplan

to save a gleisplan simply click the save button and choose the location where you want to save the file

#### open exsiting Gleisplan

to open an already build Gleisplan click on the open button and select the file you want to open

#### build New Gleisplan

if you want to build a new Gleisplan click the new button

**Achtung**
**watch out** this will delete the current Gleisplan if it is not saved

### Steuern

#### driving a train

if you want to drive a train you will need to be in driving mode
to get to driving mode click on the Steuern button 
if you want to return to build mode click the Bauen button that will appear at the position of the Steuern button

if you are in driving-mode
you can add Controls  by clicking the Steuerung-Hinzufuegen button

to drive a train you will need to type the address of the train into the textbox
now you can control the speed with the scrollbar and the light with the Licht button

if case of an emergency you can stop the train with the stop button

### Fahrplan/Code

you can write your own Fahrplan/Code using the code menu

#### Vrabiables 

you can add variables of type integer using the following syntax
  
  k = 5   
  *stores the value 5 in the variable "k"*

using this you can assing any integer-value to the variable

you can also you basic mathematical operations or other variable to assing values
  
  a = 4 + 6   
  *stores the value 10 in the variable "a"*   
  k = a + 2   
  *stores the value 12 in the variable "k"*
  
##### matheatical opperators

###### addition
you can add two values using "+"
  
  k = 2 + 3   
  *stores the value 5 in the variable "k"*

###### subtraction
you can suptract two values using "-"
  
  k = 7 - 5   
  *stores the value 2 in the variable "k"*
  
###### multiplication
you can multiply two values using "*"
  
  k = 2 * 3   
  *stores the value 6 in the variable "k"*
  
###### division
you can divide two values using "/"
  
  k = 8 / 2   
  *stores the value 4 in the variable "k"*

###### powers
you can rase a value to another values power using "^"
  
  k = 2 ^ 3   
  *stores the value 8 in the variable "k"*
                           
                         
#### if
you can use the if-operator to execute parts of code only if a condition is true

  a = 1
  
  if a = 1    
  ....a = 2
  
  *stores the value 2 in the variable "a"*
  
  a = 3
  
  if a < 2    
  ....a = 5
  
  *stores the value 3 in the variable "a"*
  
  k = 40    
  a = 1
  
  if k > 20   
  ....a = 2   
  k = 5
  
  *stores the value 2 in the variable "a"*    
  *stores the value 5 in the variable "k"*
  
##### operators
you can use the following operators for conditions

###### equal
the statement is true if both sides have equal value
  if 1 = 1
  
###### greater then
the statement is true if the left side is greater than the right side
  if 2 > 1
  
###### smaler then
the statement is true if the left side is smaler than the right side
  if 1 < 2
  
#### while
this executes the code inside the loop as long as the condition is true   

  while 1 = 1   
  ....a = 2
  
you can use the same opperators for the while condition and the if condition 

#### log
prints a value inside the ERROR-LOG

  log 5   
  *prints 5 inside the ERROR-LOG*
  
  a = 2   
  log a   
  *prints 2 inside the ERROR-LOG*
  
  log "text"    
  *prints 'text' inside the ERROR-LOG*
  
#### run
runs code at the given path
  
  run C:\thisIsAnExample.txt    
  *executes the 'thisIsAnExample' code-file*
  
#### zug
sends driving instructions to a train
  
  zug8.lichtAn = 28   
  *train number 8 turns on light and drives at speed 28*
  
  zug5.lichtAus = -16   
  *train number 5 turns off light and drives at speed 16 backwards*

#### bes
you can use the Besetztmelder as variables for conditions
  
  if bes98.3 = 1    
  ....log "train on bes98.3"
  
  if bes98.3 = 0    
  ....log "no train on bes98.3"
  
  *printes the current state of Besetztmelder 98.3 in the ERROR-LOG*
  
  
#### comments
you can wirite comments in new lines using " ' "
they will be ignored when you execute the code

  a = 5   
  'sets a to value 5
  
  a = 3   
  'sets a to value 3
  
  log a
  
  *will print 3 into the ERROR-LOG*
  
 ### ERROR-LOG
 the ERROR-LOG will show any errors occuring while running the porgram aswell as functioning as the outeput for your Fahrplan/Code
 you can clear it by clicking 'Clear all items'
 
 
