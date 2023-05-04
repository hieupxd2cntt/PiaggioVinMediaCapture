import 'package:global_configuration/global_configuration.dart';
import 'dart:io';

import 'package:flutter/foundation.dart';
//import 'package:flutter/foundation.dart';

String apiUrl = kReleaseMode
    ? "http://123.31.12.221:8024/api/"
    : "https://10.0.2.2:7262/api/";
String hostUrl =
    kReleaseMode ? "http://123.31.12.221:8022/" : "https://10.0.2.2:7015/";

//Url Api khi deploy
//const String apiUrl = "http://192.168.1.98:8024/api/";
//const String hostUrl = "http://192.168.1.98:8023/";
Future<void> deleteFile(String filePath) async {
  try {
    var file = new File(filePath);
    if (await file.exists()) {
      await file.delete();
    }
  } catch (e) {
    // Error in getting access to the file.
  }
}
