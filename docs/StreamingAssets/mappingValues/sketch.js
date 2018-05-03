function setup() {
  var maxX = 1324;
  var maxY = 993;
  var results = [];
  var locations = [[696, 880], [714, 830], [718, 785], [697, 729], [675, 668], [646, 611], [600, 555], [608, 485], [628, 429], [686, 411], [718, 358], [735, 300], [767, 243], [738, 186], [678, 172], [622, 188]];

  locations.forEach(function (arr, index) {
    var result = [];
    result.push(map(arr[0], 0, maxX, -8, 4));
    result.push(map(arr[1], 0, maxY, -4.5, 4.5));
    results.push(result);
  });
  console.log(results);
}

//16 vragen
function draw() {
  // put drawing code here
}
