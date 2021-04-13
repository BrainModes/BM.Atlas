function UnityProgress(gameInstance, progress) {
  if (!gameInstance.Module)
    return;

  if (!gameInstance.loadingBox) {
    gameInstance.loadingBox = document.createElement('div');
    gameInstance.loadingBox.id = "loadingBox";
    gameInstance.container.appendChild(gameInstance.loadingBox);
  }

  if (!gameInstance.logo) {
    gameInstance.logo = document.createElement("img");
    gameInstance.logo.id = "logo";
    gameInstance.loadingBox.appendChild(gameInstance.logo);
  }

  if (!gameInstance.bgBar) {
    gameInstance.bgBar = document.createElement("div");
    gameInstance.bgBar.id = "bgBar";
    gameInstance.loadingBox.appendChild(gameInstance.bgBar);
  }

  if (!gameInstance.progressBar) {
    gameInstance.progressBar = document.createElement("div");
    gameInstance.progressBar.id = "progressBar";
    gameInstance.loadingBox.appendChild(gameInstance.progressBar);
  }

  if (!gameInstance.loadingInfo) {
    gameInstance.loadingInfo = document.createElement("p");
    gameInstance.loadingInfo.id = "loadingInfo";
    gameInstance.loadingInfo.textContent = "The Brain Atlas is loading! \r\n One 3D interactive map of the brain coming your way... ";
    gameInstance.loadingBox.appendChild(gameInstance.loadingInfo);
  }

  var length = 200 * Math.min(progress, 1);
  gameInstance.progressBar.style.width = length + "px";
  gameInstance.loadingInfo.textContent = "The Brain Atlas is loading! \r\n One 3D interactive map of the brain coming your way...\r\n " + Math.round(progress * 100) + "% loaded.";
  if (progress == 1) {
    gameInstance.loadingBox.style.display = "none";
  }
}
