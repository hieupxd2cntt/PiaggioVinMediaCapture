import 'package:best_flutter_ui_templates/common/common.dart';
import 'package:best_flutter_ui_templates/objectmodel/users.dart';
import 'package:flutter/material.dart';
import 'dart:convert';
import 'package:http/http.dart' as http;

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
