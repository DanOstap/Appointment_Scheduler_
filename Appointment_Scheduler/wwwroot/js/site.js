let Time_clock = 0;
let TabelList = document.getElementById("TableList");
let Element_Td = document.createElement("td");
let Element_Td_Task = document.createElement("td");
let TaskCheack = false;

for (let i = 0; i <= 23; i++) {
  let Element_Th = document.createElement("th");
  let Element_Th_Task = document.createElement("th");

  Element_Th.className = "Th_Appointment_clocks";
  Element_Th_Task.className = "Th_Appointment_Task";
  Element_Th.textContent = Time_clock + ":00";
  Element_Th_Task.textContent = "";

  Element_Td.className = "Td_Clock";
  Element_Td_Task.className = "Td_Task";
  Element_Td.appendChild(Element_Th);
  Element_Td_Task.appendChild(Element_Th_Task);

  Time_clock++;
}
TabelList.appendChild(Element_Td);
TabelList.appendChild(Element_Td_Task);

function AddTaskEvent() {
  switch (TaskCheack) {
    case true:
      document.getElementById("TaskAddController").classList.remove("Visible");
      document.getElementById("TaskAddController").className = "UnVisible";
      TaskCheack = false;
      break;

    default:
      document.getElementById("TaskAddController").classList.remove("UnVisible");
      document.getElementById("TaskAddController").className = "Visible";
      TaskCheack = true;
      break;
  }
}
///// DateBase




ArrayPostInput = [];
let  flag = true, flagToTask = false;
let GetArrayTask = [];
let _Date = new Date();
let DayNow = _Date.getMonth().getDate();
let DaySelected = DayNow;
let indexbutton,Filter_Task_Array,Day;
let DayNowSelected = document.getElementById("DateList_DateNow").innerHTML;

function SelectedDate(indexButton) {
  DayNow = document.getElementById("ButtonSelect" + indexButton).innerHTML;
  indexbutton = this.indexButton;
}

function FillTask() {
  let Element_Th_clock_Array = document.getElementsByClassName("Th_Appointment_clocks");
  let Element_Th_task = document.getElementsByClassName("Th_Appointment_Task");
  let rangeStart = 0;

  $.ajax({
    url: "https://localhost:7219/tasks/",
    type: "Get",
    success: function (data) {
      GetArrayTask = data;
      Filter_Task_Array = GetArrayTask.filter((x) => x.day == DaySelected);

      for (let IndexTask = 0;IndexTask < Filter_Task_Array.length;IndexTask++) {
        for (let IndexClock = 0;IndexClock < Element_Th_clock_Array.length;IndexClock++) {
          if (flagToTask) {
            rangeStart++;
          }

          let Element_th_Index = Element_Th_task[IndexClock];
          let Task_time_from = Filter_Task_Array[IndexTask].time_from;
          let Task_time_to =  Filter_Task_Array[IndexTask].time_to;
          if (IndexClock < 10 || IndexClock >= 10) {
            if (Task_time_from[1] == IndexClock || Task_time_from[0] + Task_time_from[1] == IndexClock) 
            {
              Element_th_Index.style.background = Filter_Task_Array[IndexTask].color;
              flagToTask = true;
              Element_th_Index.style.borderTop = "dashed  3px black";
            }
            if (Task_time_to[1] == IndexClock) {
              for (rangeStart; 0 < rangeStart; rangeStart--) {
                Element_Th_task[IndexClock - rangeStart].style.background = Filter_Task_Array[IndexTask].color;
              }
              if (Task_time_to[0] + Task_time_to[1] == IndexClock) {
                Element_Th_task[IndexClock].style.borderBottom ="dashed  3px black";
                for (rangeStart; 0 <= rangeStart; rangeStart--) {
                  Element_Th_task[IndexClock - rangeStart].style.background = Element_Th_task[IndexTask].color;
                }
              }
            }
          }
        }
      }
    },
    error: function (error) {
      console.log("Eror: " + error);
    },
  });
}
function AddTaskValueToArray(ArrayInputPostToArray) {
  ArrayInputPostToArray.forEach((element) => {
    ArrayPostInput.push(document.getElementById(element).value);
  });
}
function AddTask() {
  let ArrayInput = [
    "Task_Ttile_Post",
    "Task_Description_Post",
    "Task_TimeFrom_Time_Post",
    "Task_TimeTo_Time_Post",
    "Task_color_Post",
  ];
  AddTaskValueToArray(ArrayInput);
  for (let index = 0; index < ArrayPostInput.length; index++) {
    if (index != 1) {
      if (ArrayPostInput[index] == "" || ArrayPostInput[index] == null) {
        alert(ArrayInput[index] + " Cant be empty");
        flag = false;
      }
    }
  }
  if (flag) {
    DayNowSelected = DayNowSelected * 1;
    $.ajax({
      url: "https://localhost:7219/tasks/",
      type: "Post",
      contentType: "application/json; charset=UTF-8",
      data: JSON.stringify({
        title: ArrayPostInput[0],
        description: ArrayPostInput[1],
        time_from: ArrayPostInput[2],
        time_to: ArrayPostInput[3],
        color: ArrayPostInput[4],
        day: DayNowSelected,
        month: _Date.getMonth() + 1,
      }),
      success: function (data) {
        console.log("Post work");
      },
      error: function (error) {
        console.log(error);
      },
    });
  }
}
FillTask();
