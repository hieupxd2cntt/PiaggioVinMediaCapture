import 'dart:io';

import 'package:VinMediaCapture/common/common.dart';
import 'package:VinMediaCapture/login/Toast.dart';
import 'package:VinMediaCapture/model/doc_type_model_list_data.dart';
import 'package:VinMediaCapture/objectmodel/doctypeguideinsertmodel.dart';
import 'package:VinMediaCapture/objectmodel/users.dart';
import 'package:dio/dio.dart';
import 'package:flutter/foundation.dart';
import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:flutter_session_manager/flutter_session_manager.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;
import 'package:path/path.dart';
import 'package:platform_device_id/platform_device_id.dart';

Future<http.Response> GetLogin(Users user) async {
  var response = await http.post(
    Uri.parse(apiUrl + 'User/Login'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(user),
  );
  return response;
}

Future<http.Response> GetListAttribute(String barcode) async {
  var response = await http.post(
    Uri.parse(apiUrl + 'Mobile/GetListAttribute'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(barcode),
  );
  return response;
}

Future<http.Response> GetListAttributeByVinCode(String vinCode) async {
  var response = await http.post(
    Uri.parse(apiUrl + 'Mobile/GetListAttributeByVinCode'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(vinCode),
  );
  return response;
}

Future<Response> PostDocTypeGuideItem(
    List<DocTypeModelListData> modelList) async {
  List<MultipartFile> images = [];
  for (var element in modelList) {
    if (element.assetImage.length > 0) {
      var image = await MultipartFile.fromFile(element.assetImage,
          filename: basename(element.assetImage));
      images.add(image);
    }
  }

  FormData formData = new FormData.fromMap({
    "docTypeGuides": json.encode(modelList),
    "images": images,
  });
  String? deviceId = "";
  try {
    deviceId = await PlatformDeviceId.getDeviceId;
  } on PlatformException {
    deviceId = 'Failed to get deviceId.';
  }
  var sessionManager = SessionManager();
  String loginName = await sessionManager.get("loginName");
  var dio = new Dio();
  dio.options.headers['Authorization'] = 'Bearer';
  dio.options.headers["deviceId"] = deviceId;
  dio.options.headers["UserName"] = loginName;

  Response response =
      await dio.post(apiUrl + 'Mobile/InsertDocTypeGuide', data: formData);
  /*Response response = await Dio().post(
    apiUrl + 'Mobile/InsertDocTypeGuide',
    data: formData,
    options: Options(headers: <String, String>{
      'Authorization': 'Bearer',
    }),
  );*/
  return response;

  /*var response = await http.post(
    Uri.parse(apiUrl + 'Mobile/InsertDocTypeGuide'),
    headers: <String, String>{
      'Content-Type': 'application/json; charset=UTF-8',
    },
    body: json.encode(docTypeGuide),
  );
  return response;*/
}
