#!/bin/bash

# API Testing Script for AudioPool Authentication
BASE_URL="http://localhost:5146"
TOKEN="AudioPoolSecretToken2024"

echo "🧪 AudioPool API Authentication Test"
echo "===================================="
echo

# Colors for output
RED='\033[0;31m'
GREEN='\033[0;32m'
YELLOW='\033[1;33m'
NC='\033[0m' # No Color

# Function to test endpoint
test_endpoint() {
    local method=$1
    local endpoint=$2
    local data=$3
    local expected_status=$4
    local auth_header=$5
    local description=$6
    
    echo -n "Testing $description... "
    
    if [ -n "$auth_header" ]; then
        if [ -n "$data" ]; then
            response=$(curl -s -w "HTTPSTATUS:%{http_code}" -X "$method" \
                "$BASE_URL$endpoint" \
                -H "Content-Type: application/json" \
                -H "api-token: $TOKEN" \
                -d "$data")
        else
            response=$(curl -s -w "HTTPSTATUS:%{http_code}" -X "$method" \
                "$BASE_URL$endpoint" \
                -H "api-token: $TOKEN")
        fi
    else
        if [ -n "$data" ]; then
            response=$(curl -s -w "HTTPSTATUS:%{http_code}" -X "$method" \
                "$BASE_URL$endpoint" \
                -H "Content-Type: application/json" \
                -d "$data")
        else
            response=$(curl -s -w "HTTPSTATUS:%{http_code}" -X "$method" \
                "$BASE_URL$endpoint")
        fi
    fi
    
    # Extract status code
    status=$(echo $response | tr -d '\n' | sed -e 's/.*HTTPSTATUS://')
    
    if [ "$status" = "$expected_status" ]; then
        echo -e "${GREEN}✅ PASS${NC} (Status: $status)"
    else
        echo -e "${RED}❌ FAIL${NC} (Expected: $expected_status, Got: $status)"
    fi
}

echo "📍 Step 1: Testing GET endpoints (should work WITHOUT auth)"
echo "--------------------------------------------------------"
test_endpoint "GET" "/genres" "" "200" "" "GET /genres"
test_endpoint "GET" "/genres/1" "" "200" "" "GET /genres/1"
test_endpoint "GET" "/artists" "" "200" "" "GET /artists"
test_endpoint "GET" "/artists/1" "" "200" "" "GET /artists/1"
test_endpoint "GET" "/artists/1/albums" "" "200" "" "GET /artists/1/albums"
test_endpoint "GET" "/albums/3" "" "200" "" "GET /albums/3"
test_endpoint "GET" "/albums/3/songs" "" "200" "" "GET /albums/3/songs"
test_endpoint "GET" "/songs/28" "" "200" "" "GET /songs/28"

echo
echo "🔒 Step 2: Testing protected endpoints WITHOUT auth (should fail)"
echo "----------------------------------------------------------------"
test_endpoint "POST" "/genres" '{"name":"Test Genre"}' "401" "" "POST /genres (no auth)"
test_endpoint "POST" "/artists" '{"name":"Test Artist","bio":"Test bio","dateOfStart":"2020-01-01T00:00:00.000Z"}' "401" "" "POST /artists (no auth)"
test_endpoint "PUT" "/artists/1" '{"name":"Updated Artist","bio":"Updated bio","dateOfStart":"2020-01-01T00:00:00.000Z"}' "401" "" "PUT /artists/1 (no auth)"
test_endpoint "PATCH" "/artists/1/genres/1" "" "401" "" "PATCH /artists/1/genres/1 (no auth)"
test_endpoint "POST" "/albums" '{"name":"Test Album","releaseDate":"2024-01-01T00:00:00.000Z","artistIds":[1]}' "401" "" "POST /albums (no auth)"
test_endpoint "DELETE" "/albums/1" "" "401" "" "DELETE /albums/1 (no auth)"
test_endpoint "POST" "/songs" '{"name":"Test Song","duration":"00:03:30","albumId":1}' "401" "" "POST /songs (no auth)"
test_endpoint "PUT" "/songs/1" '{"name":"Updated Song","duration":"00:03:45","albumId":1}' "401" "" "PUT /songs/1 (no auth)"
test_endpoint "DELETE" "/songs/1" "" "401" "" "DELETE /songs/1 (no auth)"

echo
echo "🔑 Step 3: Testing protected endpoints WITH auth (should work)"
echo "------------------------------------------------------------"
test_endpoint "POST" "/genres" '{"name":"Test Genre Script"}' "201" "yes" "POST /genres (with auth)"
test_endpoint "POST" "/artists" '{"name":"Test Artist Script","bio":"Test bio from script","dateOfStart":"2020-01-01T00:00:00.000Z"}' "201" "yes" "POST /artists (with auth)"
test_endpoint "PUT" "/artists/2" '{"name":"Updated Artist Script","bio":"Updated bio from script","dateOfStart":"2020-01-01T00:00:00.000Z"}' "204" "yes" "PUT /artists/2 (with auth)"
test_endpoint "PATCH" "/artists/2/genres/1" "" "204" "yes" "PATCH /artists/2/genres/1 (with auth)"
test_endpoint "POST" "/albums" '{"name":"Test Album Script","releaseDate":"2024-01-01T00:00:00.000Z","artistIds":[2]}' "201" "yes" "POST /albums (with auth)"
test_endpoint "POST" "/songs" '{"name":"Test Song Script","duration":"00:03:30","albumId":3}' "201" "yes" "POST /songs (with auth)"

echo
echo "🎉 Testing Complete!"
echo "==================="
echo "If you see mostly ✅ PASS results, your authentication is working correctly!"
echo "❌ FAIL results indicate issues that need to be fixed."